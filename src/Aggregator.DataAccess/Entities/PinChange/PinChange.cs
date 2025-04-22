using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public class PinChange : Notification
{
    /// <summary>
    /// Уникальный идентификатор PinChangeDetails
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="PinChangeDetails" />
    public PinChangeDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long CardInfoId { get; set; }

    /// <inheritdoc cref="CardInfo" />
    public CardInfo CardInfo { get; set; }
}