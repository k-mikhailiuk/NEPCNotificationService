namespace DataIngrestorApi.DTOs;

/// <summary>
/// Количественный лимит
/// </summary>
public class CntLimitDto
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
    /// Пороговое (выставленное) значение лимита
    /// </summary>
    public long TrsValue { get; set; }
    
    /// <summary>
    /// Текущее (накопленное) значение по лимиту
    /// </summary>
    public long UsedValue { get; set; }
}