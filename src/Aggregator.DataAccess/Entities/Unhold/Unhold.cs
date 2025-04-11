using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.Unhold;

/// <summary>
/// Уведомление о снятии холда
/// </summary>
public class Unhold : INotification
{
    /// <inheritdoc />
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NotificationId { get; set; }
    
    /// <inheritdoc />
    public NotificationType NotificationType { get; set; }
    
    /// <inheritdoc />
    public long EventId { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// Уникальный идентификатор UnholdDetails 
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="UnholdDetails" />
    public UnholdDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    public CardInfo CardInfo { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор MerchantInfo
    /// </summary>
    public long MerchantInfoId { get; set; }
    
    /// <inheritdoc cref="MerchantInfo" />
    public MerchantInfo MerchantInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}