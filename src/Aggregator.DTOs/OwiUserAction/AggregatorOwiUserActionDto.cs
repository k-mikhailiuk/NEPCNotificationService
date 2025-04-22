using Aggregator.DTOs.Abstractions;

namespace Aggregator.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public record AggregatorOwiUserActionDto : INotificationAggregatorDto
{
    /// <inheritdoc />
    public long Id { get; init; }
    
    /// <inheritdoc />
    public long EventId { get; init; }
    
    /// <inheritdoc />
    public string Time { get; init; }
    
    /// <summary>
    /// Подробная информация о действии пользователя в OWI
    /// </summary>
    public AggregatorOwiUserActionDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; init; }
}