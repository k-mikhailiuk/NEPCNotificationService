using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public class CardStatusChange
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Key]
    public long CardStatusChangeId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    [Required]
    public long EventId { get; set; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    [Required]
    public DateTimeOffset Time { get; set; }
    
    /// <summary>
    /// Идентификатор CardStatusChangeDetails
    /// </summary>
    [Required]
    public long CardStatusChangeDetailsId { get; set; }
    
    /// <inheritdoc cref="CardStatusChangeDetails" />
    [ForeignKey(nameof(CardStatusChangeDetailsId))]
    public CardStatusChangeDetails CardStatusChangeDetails { get; set; }
    
    /// <summary>
    /// Идентификатор CardInfo
    /// </summary>
    [Required]
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    [ForeignKey(nameof(CardInfoId))]
    public CardInfo CardInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}