using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.Core.Services;

public class AcqFinAuthNotificationMessageBuilder : INotificationMessageBuilder<AcqFinAuth>
{
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds, CancellationToken cancellationToken)
    {
        var list = notificationIds.Select(notificationId => new NotificationMessage
            { Title = $"{notificationId}", Message = "test", Status = NotificationMessageStatus.New }).ToList();
        
        return list;
    }
}