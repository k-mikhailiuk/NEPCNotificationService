namespace Aggregator.DTOs;

/// <summary>
/// Суммовой лимит
/// </summary>
public class AggregatorAmtLimitDto
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Тип цикла лимита
    /// </summary>
    public string? CycleType { get; set; }
    
    /// <summary>
    /// Длина цикла лимита
    /// </summary>
    public int? CycleLength { get; set; }
    
    /// <summary>
    /// Дата окончания цикла (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string? EndTime { get; set; }
    
    /// <summary>
    /// Трехзначный числовой код валюты (ISO-4217)
    /// </summary>
    public string Currency { get; set; }
    
    /// <summary>
    /// Пороговое (выставленное) значение лимита
    /// </summary>
    public long TrsAmount { get; set; }
    
    /// <summary>
    /// Текущее (накопленное) значение по лимиту
    /// </summary>
    public long UsedAmount { get; set; }
}