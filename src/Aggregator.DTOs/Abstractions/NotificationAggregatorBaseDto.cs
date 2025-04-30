namespace Aggregator.DTOs.Abstractions;

public record NotificationAggregatorBaseDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long Id { get; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string Time { get; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public IEnumerable<AggregatorExtensionDto>? Extensions { get; }
}