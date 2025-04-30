namespace Aggregator.DTOs;

/// <summary>
/// Количественный лимит
/// </summary>
public record AggregatorCntLimitDto
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Тип цикла лимита
    /// </summary>
    public string? CycleType { get; init; }
    
    /// <summary>
    /// Длина цикла лимита
    /// </summary>
    public int? CycleLength { get; init; }
    
    /// <summary>
    /// Дата окончания цикла (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string? EndTime { get; init; }
    
    /// <summary>
    /// Пороговое (выставленное) значение лимита
    /// </summary>
    public long TrsValue { get; init; }
    
    /// <summary>
    /// Текущее (накопленное) значение по лимиту
    /// </summary>
    public long UsedValue { get; init; }
}