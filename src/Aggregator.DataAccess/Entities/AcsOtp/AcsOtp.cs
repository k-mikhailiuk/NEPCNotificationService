using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.AcsOtp;

public class AcsOtp : INotification
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NotificationId { get; set; }
    
    public NotificationType NotificationType { get; set; }
    
    public long EventId { get; set; }
    
    public DateTimeOffset Time { get; set; }
    
    public AcsOtpDetails Details { get; set; }
    
    public long CardInfoId { get; set; }
    
    public CardInfo CardInfo { get; set; }
    
    public AcsOtpMerchantInfo MerchantInfo { get; set; }
    
    public List<NotificationExtension>? Extensions { get; set; }
}