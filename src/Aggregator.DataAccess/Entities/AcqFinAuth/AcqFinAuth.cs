using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.AcqFinAuth;

/// <summary>
/// Уведомление об эквайринговой финансовой авторизации по карте
/// </summary>
public class AcqFinAuth
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Key]
    public long AcqFinAuthId { get; set; }
    
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
    /// Идентификатор деталей онлайн эквайринговой финансовой авторизации по карте
    /// </summary>
    [Required]
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="AcqFinAuthDetails" />
    [ForeignKey(nameof(DetailsId))]
    public AcqFinAuthDetails Details { get; set; }
    
    [Required]
    public long MerchantInfoId { get; set; }

    /// <inheritdoc cref="MerchantInfo" />
    [ForeignKey(nameof(MerchantInfoId))]
    public MerchantInfo MerchantInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension> Extensions { get; set; }
}