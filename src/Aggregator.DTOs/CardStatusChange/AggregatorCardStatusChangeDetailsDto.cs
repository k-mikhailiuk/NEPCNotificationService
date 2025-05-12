namespace Aggregator.DTOs.CardStatusChange;

/// <summary>
/// Подробная информация об изменении статуса карты
/// </summary>
public record AggregatorCardStatusChangeDetailsDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; init; }
    
    /// <summary>
    /// Старое значение статуса
    /// </summary>
    public int OldStatus { get; init; }
    
    /// <summary>
    /// Новое значение статуса
    /// </summary>
    public int NewStatus { get; init; }
    
    /// <summary>
    /// Дата изменения статуса карты в ПЦ в формате (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate { get; init; }
    
    /// <summary>
    /// Сервис, изменивший статус карты
    /// </summary>
    public string? Service { get; init; }
    
    /// <summary>
    /// Пользователь сервиса, изменивший статус карты
    /// </summary>
    public string? UserName { get; init; }
    
    /// <summary>
    /// Причина изменения статуса карты
    /// </summary>
    public string? Note { get; init; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public IEnumerable<AggregatorCardIdentifierDto>? CardIdentifier { get; init; }
}