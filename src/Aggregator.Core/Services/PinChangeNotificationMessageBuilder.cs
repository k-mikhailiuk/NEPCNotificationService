using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.PinChange;

namespace Aggregator.Core.Services;

public class PinChangeNotificationMessageBuilder : INotificationMessageBuilder<PinChange>
{
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds, CancellationToken cancellationToken)
    {
        var list = notificationIds.Select(notificationId => new NotificationMessage
            { Title = $"{notificationId}", Message = "test", Status = NotificationMessageStatus.New }).ToList();
        
        return list;
    }
}