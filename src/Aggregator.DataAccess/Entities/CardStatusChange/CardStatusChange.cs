namespace Aggregator.DataAccess.Entities.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public class CardStatusChange
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long CardStatusChangeId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset Time { get; set; }
    
    /// <summary>
    /// Идентификатор CardStatusChangeDetails
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="Details" />
    public CardStatusChangeDetails Details { get; set; }
    
    /// <summary>
    /// Идентификатор CardInfo
    /// </summary>
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    public CardInfo CardInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}