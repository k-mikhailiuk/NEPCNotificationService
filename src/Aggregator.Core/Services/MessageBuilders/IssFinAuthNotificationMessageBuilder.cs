using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

public class IssFinAuthNotificationMessageBuilder : INotificationMessageBuilder<IssFinAuth>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyWordBuilder<IssFinAuth> _keyWordBuilder;
    private readonly ILanguageSelector _languageSelector;
    
    public IssFinAuthNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions,
        IServiceProvider serviceProvider, IKeyWordBuilder<IssFinAuth> keyWordBuilder, ILanguageSelector languageSelector)
    {
        _serviceProvider = serviceProvider;
        _keyWordBuilder = keyWordBuilder;
        _languageSelector = languageSelector;
        _notificationMessageOptions = notificationMessageOptions.Value;
    }

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = _serviceProvider.CreateScope();
        
        using var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

        if (unitOfWork == null)
            throw new ArgumentNullException(nameof(unitOfWork));
        
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var messages =
            await unitOfWork.IssFinAuth.GetListByIdsRawSqlAsync(
                notificationIds,
                cancellationToken,
                x => x.Details,
                x => x.CardInfo,
                x => x.MerchantInfo,
                x => x.CardInfo.Limits);

        foreach (var message in messages)
        {
            foreach (var limit in message.CardInfo.Limits)
            {
                limit.Limit = await unitOfWork.Limit.GetByIdAsync(limit.LimitId, cancellationToken) ??
                              throw new InvalidOperationException();
            }

            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.IssFinAuth &&
                     (int)x.OperationType! == message.Details.TransType, cancellationToken);

            if (messageText == null)
                continue;

            if (!messageText.IsNeedSend)
                continue;

            var customerId = await GetCustomerId(message.Details.AccountId, context, cancellationToken);

            if(customerId == null)
                continue;
            
            var languageId = await _languageSelector.GetLanguageId(customerId.Value, cancellationToken);

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
                Message = await _keyWordBuilder.BuildKeyWordsAsync(localizeMessage, message, language),
                CustomerId = customerId.Value,
            };

            list.Add(notificationMessage);
        }

        return list;
    }

    private static async Task<long?> GetCustomerId(string accountId, AggregatorDbContext context, CancellationToken cancellationToken)
    {
        await using var connection = context.Database.GetDbConnection();

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