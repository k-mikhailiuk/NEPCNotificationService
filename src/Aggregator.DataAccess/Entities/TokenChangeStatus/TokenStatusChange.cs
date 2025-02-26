using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.TokenChangeStatus;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public class TokenStatusChange
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Key]
    public long TokenChangeStatusId { get; set; }
    
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
    /// Уникальный идентификатор TokenStatusChangeDetails
    /// </summary>
    [Required]
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="TokenStatusChangeDetails" />
    [ForeignKey(nameof(DetailsId))]
    public TokenStatusChangeDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    [Required]
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    [ForeignKey(nameof(CardInfoId))]
    public CardInfo CardInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}