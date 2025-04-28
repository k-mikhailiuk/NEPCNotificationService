using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Common.Enums;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель сообщений уведомлений для AcctBalChange.
/// Формирует объект <see cref="NotificationMessage"/> на основе уведомления AcctBalChange, используя локализованный шаблон сообщения.
/// </summary>
public class AcctBalChangeNotificationMessageBuilder(
    IOptions<NotificationMessageOptions> notificationMessageOptions,
    IServiceProvider serviceProvider,
    IKeyWordBuilder<AcctBalChange> keyWordBuilder,
    ICustomerIdSelector customerIdSelector)
    : INotificationMessageBuilder<AcctBalChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions = notificationMessageOptions.Value;

    /// <summary>
    /// Асинхронно строит список сообщений уведомлений для заданных идентификаторов уведомлений AcctBalChange.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений AcctBalChange.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объектов <see cref="NotificationMessage"/>.</returns>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        var messages =
            await unitOfWork.AcctBalChange.GetByIdsWithIncludesAsync(notificationIds,
                cancellationToken, 
                x => x.Details,
                x=>x.CardInfo);
        
        var operationTypes = messages.Select(m => m.Details.TransType)
            .Distinct()
            .ToList();

        if (operationTypes.Count == 0)
            throw new ArgumentNullException($"{operationTypes} is null");
        
        var messageTextMap = await context.NotificationMessageTextDirectories
            .Where(x =>
                x.NotificationType == NotificationMessageType.AcctBalChange &&
                operationTypes.Contains((int)x.OperationType!))
            .ToDictionaryAsync(
                x => (int)x.OperationType!,
                x => x,
                cancellationToken
            );
        
        var allAccountIds = messages.Select(m => m.Details.AccountId);
        var accountIdMap = await customerIdSelector
            .GetCustomerIdsAsync(allAccountIds, context, cancellationToken);
        
        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountIdMap.TryGetValue(m.Details.AccountId, out var cid) 
                    ? cid 
                    : null
            );

        foreach (var message in messages)
        {
            if (!messageTextMap.TryGetValue(message.Details.TransType, out var messageText) && messageText is { IsNeedSend: false })
                continue;
            
            var customerId = await customerIdSelector.GetCustomerIdAsync(message.Details.AccountId, context, cancellationToken);

            if(customerId == null)
                continue;

            var languageSelector = scope.ServiceProvider.GetRequiredService<ILanguageSelector>();
            
            var languageId = await languageSelector.GetLanguageIdAsync(customerId.Value, context, cancellationToken);

            var language = languageId.HasValue ? (Language)languageId.Value : Language.Russian;
            
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
}