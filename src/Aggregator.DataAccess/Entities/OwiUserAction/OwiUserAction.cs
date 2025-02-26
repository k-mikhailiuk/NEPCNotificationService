using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public class OwiUserAction
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Key]
    public long OwiUserActionId { get; set; }
    
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
    /// Уникальный идентификатор OwiUserActionDetails
    /// </summary>
    [Required]
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="OwiUserActionDetails" />
    [ForeignKey(nameof(DetailsId))]
    public OwiUserActionDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long? CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    [ForeignKey(nameof(CardInfoId))]
    public CardInfo? CardInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}