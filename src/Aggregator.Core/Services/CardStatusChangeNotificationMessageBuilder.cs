using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.Core.Services;

public class CardStatusChangeNotificationMessageBuilder : INotificationMessageBuilder<CardStatusChange>
{
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds, CancellationToken cancellationToken)
    {
        var list = notificationIds.Select(notificationId => new NotificationMessage
            { Title = $"{notificationId}", Message = "test", Status = NotificationMessageStatus.New }).ToList();
        
        return list;
    }
}