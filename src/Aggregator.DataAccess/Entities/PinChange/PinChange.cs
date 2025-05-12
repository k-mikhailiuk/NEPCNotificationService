using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public class PinChange : Notification
{
    /// <inheritdoc cref="PinChangeDetails" />
    public PinChangeDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long CardInfoId { get; set; }

    /// <inheritdoc cref="CardInfo" />
    public CardInfo CardInfo { get; set; }
}