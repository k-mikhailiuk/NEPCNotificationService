using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public class PinChange
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Key]
    public long PinChangeId { get; set; }
    
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
    /// Уникальный идентификатор PinChangeDetails
    /// </summary>
    [Required]
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="PinChangeDetails" />
    [ForeignKey(nameof(DetailsId))]
    public PinChangeDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    [Required]
    public long CardInfoId { get; set; }

    /// <inheritdoc cref="CardInfo" />
    [ForeignKey(nameof(CardInfoId))]
    public CardInfo CardInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension> Extensions { get; set; }
}