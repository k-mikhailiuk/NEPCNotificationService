namespace Aggregator.DTOs.Abstractions;

public record NotificationAggregatorDto<TDetails> : NotificationAggregatorBaseDto
{
    /// <summary>
    /// Детали уведомления
    /// </summary>
    public TDetails Details { get; init; }
}