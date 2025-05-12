using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.TokenChangeStatus;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public class TokenStatusChange : Notification
{
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