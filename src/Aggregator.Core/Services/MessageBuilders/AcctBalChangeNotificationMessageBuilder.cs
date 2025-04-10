using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.Repositories.Abstractions;
using Common.Enums;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

public class AcctBalChangeNotificationMessageBuilder(
    IOptions<NotificationMessageOptions> notificationMessageOptions,
    IServiceProvider serviceProvider,
    IKeyWordBuilder<AcctBalChange> keyWordBuilder)
    : INotificationMessageBuilder<AcctBalChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions = notificationMessageOptions.Value;

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        await using var connection = context.Database.GetDbConnection(); 

        if (unitOfWork == null)
            throw new ArgumentNullException(nameof(unitOfWork));
        
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var messages =
            await unitOfWork.AcctBalChange.GetListByIdsRawSqlAsync(notificationIds,
                cancellationToken, 
                x => x.Details,
                x=>x.CardInfo);

        foreach (var message in messages)
        {
            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.AcctBalChange &&
                     (int)x.OperationType! == message.Details.TransType, cancellationToken);

            if (messageText == null)
                continue;

            if (!messageText.IsNeedSend)
                continue;
            
            var customerId = await GetCustomerIdAsync(message.Details.AccountId, context, cancellationToken);

            if(customerId == null)
                continue;

            var languageSelector = scope.ServiceProvider.GetRequiredService<ILanguageSelector>();
            
            var languageId = await languageSelector.GetLanguageIdAsync(customerId.Value, context, cancellationToken);

            var language = Language.Russian;
            
            if(languageId != null)
                language = (Language)languageId;
            
            if (language == Language.Undefined)
                continue;
            
            var localizeMessage = language switch
            {
                Language.Russian => messageText.MessageTextRu,
                Language.English => messageText.MessageTextEn,
                Language.Kyrgyz => messageText.MessageTextKg,
                _ => messageText.MessageTextRu
            };
            
            var notificationMessage = new NotificationMessage
            {
                Title = _notificationMessageOptions.Title,
                Status = NotificationMessageStatus.New,
                Message = await keyWordBuilder.BuildKeyWordsAsync(localizeMessage, message, language),
                CustomerId = customerId.Value,
            };

            list.Add(notificationMessage);
        }

        return list;
    }
    
    private static async Task<long?> GetCustomerIdAsync(string accountId, AggregatorDbContext context, CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
            
        command.CommandText = @"
        SELECT accounts.CustomerID
        FROM dbo.Accounts accounts 
        WHERE AccountNo = SUBSTRING(CAST(@accountId AS VARCHAR(50)), 4, LEN(CAST(@accountId AS VARCHAR(50))) - 6)";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@accountId";
        parameter.Value = accountId;
        command.Parameters.Add(parameter);
            
        var result = await command.ExecuteScalarAsync(cancellationToken);
    
        if(result == null || result == DBNull.Value)
            return null;
    
        return Convert.ToInt64(result);
    }
}