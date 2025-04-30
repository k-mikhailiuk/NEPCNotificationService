using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public record AggregatorCardStatusChangeDto : NotificationAggregatorDto<AggregatorCardStatusChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
}