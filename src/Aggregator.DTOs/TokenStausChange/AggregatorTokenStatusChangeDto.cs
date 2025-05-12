using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.TokenStausChange;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public record AggregatorTokenStatusChangeDto : NotificationAggregatorDto<AggregatorTokenStatusChangeDetailsDto>
{
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
}