using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.TokenStausChange;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public record AggregatorTokenStatusChangeDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Подробная информация об изменении статуса токена
    /// </summary>
    public AggregatorTokenStatusChangeDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}