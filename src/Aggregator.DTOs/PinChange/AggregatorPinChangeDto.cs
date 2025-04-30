using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public record AggregatorPinChangeDto : NotificationAggregatorDto<AggregatorPinChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
}