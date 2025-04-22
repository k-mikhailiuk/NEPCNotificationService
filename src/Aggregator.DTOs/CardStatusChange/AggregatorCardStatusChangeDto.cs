using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public record AggregatorCardStatusChangeDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Подробная информация об изменении статуса карты
    /// </summary>
    public AggregatorCardStatusChangeDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}