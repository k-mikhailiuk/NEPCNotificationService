using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

public class CardStatusChangeNotificationMessageBuilder : INotificationMessageBuilder<CardStatusChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyWordBuilder<CardStatusChange> _keyWordBuilder;
    private readonly ILanguageSelector _languageSelector;

    public CardStatusChangeNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions,
        IServiceProvider serviceProvider, IKeyWordBuilder<CardStatusChange> keyWordBuilder, ILanguageSelector languageSelector)
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
            await unitOfWork.CardStatusChange.GetListByIdsRawSqlAsync(notificationIds, cancellationToken,
                x => x.Details);

        foreach (var message in messages)
        {
            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
            x => x.NotificationType == NotificationMessageType.CardStatusChange, cancellationToken);

        if (messageText == null)
            throw new NullReferenceException();

        if (!messageText.IsNeedSend)
            return list;

            var customerId = await GetCustomerId(message.Details.CardIdentifier.CardIdentifierValue, context, cancellationToken);

            if(customerId == null)
                continue;
            
            var language = (Language)await _languageSelector.GetLanguageId(customerId.Value, context, cancellationToken);
            
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
    
    private async Task<long?> GetCustomerId(string accountId, AggregatorDbContext context, CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
            
        command.CommandText = @"
        SELECT accounts.CustomerID
        FROM dbo.Accounts accounts 
        WHERE AccountNo = SUBSTRING(CAST(@accountId AS VARCHAR(50)), 4, LEN(CAST(@accountId AS VARCHAR(50))) - 11)";

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