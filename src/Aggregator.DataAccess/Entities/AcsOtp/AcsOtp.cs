using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.AcsOtp;

public class AcsOtp : INotification
{
    public long NotificationId { get; set; }
    
    public NotificationType NotificationType { get; set; }
    
    public long EventId { get; set; }
    
    public DateTimeOffset Time { get; set; }
    
    public List<NotificationExtension>? Extensions { get; set; }
}