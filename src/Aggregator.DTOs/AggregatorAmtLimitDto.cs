namespace Aggregator.DTOs;

/// <summary>
/// Суммовой лимит
/// </summary>
public record AggregatorAmtLimitDto
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
    /// Трехзначный числовой код валюты (ISO-4217)
    /// </summary>
    public string Currency { get; init; }
    
    /// <summary>
    /// Пороговое (выставленное) значение лимита
    /// </summary>
    public long TrsAmount { get; init; }
    
    /// <summary>
    /// Текущее (накопленное) значение по лимиту
    /// </summary>
    public long UsedAmount { get; init; }
}