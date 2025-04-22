using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.PinChange;

/// <summary>
/// Уведомление об изменении PIN-кода
/// </summary>
public record AggregatorPinChangeDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Подробная информация об изменении PIN-кода
    /// </summary>
    public AggregatorPinChangeDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}