using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.AcqFinAuth;

/// <summary>
/// Уведомление об эквайринговой финансовой авторизации по карте
/// </summary>
public class AcqFinAuth : INotification
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
    /// Идентификатор деталей онлайн эквайринговой финансовой авторизации по карте
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="AcqFinAuthDetails" />
    public AcqFinAuthDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор MerchantInfo
    /// </summary>
    public long MerchantInfoId { get; set; }

    /// <inheritdoc cref="MerchantInfo" />
    public MerchantInfo MerchantInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
    
}