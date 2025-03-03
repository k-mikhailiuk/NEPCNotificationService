using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.TokenStausChange;

/// <summary>
/// Уведомление об изменении статуса токена
/// </summary>
public class AggregatorTokenStausChangeDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; set; }
    
    /// <inheritdoc />
    public long EventId { get; set; }
    
    /// <inheritdoc />
    public string Time { get; set; }
    
    /// <summary>
    /// Подробная информация об изменении статуса токена
    /// </summary>
    public AggregatorTokenStausChangeDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}