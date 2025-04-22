using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public class CardStatusChange : Notification
{
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
}