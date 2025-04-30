using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public record AggregatorOwiUserActionDto : NotificationAggregatorDto<AggregatorOwiUserActionDetailsDto>
{
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; init; }
}