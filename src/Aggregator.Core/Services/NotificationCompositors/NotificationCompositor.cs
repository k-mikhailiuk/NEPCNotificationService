using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Common.Enums;
using ControlPanel.DataAccess.Entities;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.NotificationCompositors;

public class NotificationCompositor<T>(
    IOptions<NotificationMessageOptions> notificationMessageOptions,
    IKeyWordBuilder<T> keyWordBuilder) : INotificationCompositor<T> where T : Notification
{
    private readonly NotificationMessageOptions _notificationMessageOptions = notificationMessageOptions.Value;

    public async Task<List<NotificationMessage>> ComposeAsync(IEnumerable<T> messages,
        Dictionary<long, NotificationMessageTextDirectory> notificationTextById,
        Dictionary<long, int> notificationToCustomer,
        Dictionary<int, int> customerSettingsMap, CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        foreach (var message in messages)
        {
            notificationTextById.TryGetValue(message.NotificationId, out var messageText);

            if (messageText == null)
                continue;

            if (!messageText.IsNeedSend)
                continue;

            notificationToCustomer.TryGetValue(message.NotificationId, out var customerId);

            var language = customerSettingsMap.TryGetValue(customerId, out var languageId)
                ? (Language)languageId
                : Language.Russian;

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
                Message =
                    await keyWordBuilder.BuildKeyWordsAsync(localizeMessage, message, language, cancellationToken),
                CustomerId = customerId,
            };

            list.Add(notificationMessage);
        }

        return list;
    }
}