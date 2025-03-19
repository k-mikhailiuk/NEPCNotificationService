using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services;

public class CardStatusChangeNotificationMessageBuilder : INotificationMessageBuilder<CardStatusChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;

    public CardStatusChangeNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions)
    {
        _notificationMessageOptions = notificationMessageOptions.Value;
    }

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = notificationIds.Select(notificationId => new NotificationMessage
                { Title = _notificationMessageOptions.Title, Message = "test", Status = NotificationMessageStatus.New })
            .ToList();

        return list;
    }
}