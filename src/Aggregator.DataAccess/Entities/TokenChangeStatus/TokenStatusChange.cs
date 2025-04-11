using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.TokenChangeStatus;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public class TokenStatusChange : INotification
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
    /// Уникальный идентификатор TokenStatusChangeDetails
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="TokenStatusChangeDetails" />
    public TokenStatusChangeDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    public CardInfo CardInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}