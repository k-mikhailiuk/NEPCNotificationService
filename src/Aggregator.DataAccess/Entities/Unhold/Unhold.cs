using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.Unhold;

/// <summary>
/// Уведомление о снятии холда
/// </summary>
public class Unhold
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Key]
    public long UnholdId { get; set; }
    
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
    /// Уникальный идентификатор UnholdDetails 
    /// </summary>
    [Required]
    public long UnholdDetailsId { get; set; }
    
    /// <inheritdoc cref="UnholdDetails" />
    [ForeignKey(nameof(UnholdDetailsId))]
    public UnholdDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    [Required]
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    [ForeignKey(nameof(CardInfoId))]
    public CardInfo CardInfo { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор MerchantInfo
    /// </summary>
    [Required]
    public long MerchantInfoId { get; set; }
    
    /// <inheritdoc cref="MerchantInfo" />
    [ForeignKey(nameof(MerchantInfoId))]
    public MerchantInfo MerchantInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extesions { get; set; }
}