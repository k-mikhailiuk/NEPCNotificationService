using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Лимит
/// </summary>
public class Limit
{
    /// <summary>
    /// Уникальный идентификатор для бд
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long LimitId { get; set; }
    
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
    public DateTimeOffset? EndTime { get; set; }
    
    /// <summary>
    /// Трехзначный числовой код валюты (ISO-4217)
    /// </summary>
    public string? Currency { get; set; }
    
    /// <summary>
    /// Пороговое (выставленное) значение лимита
    /// </summary>
    [Column(TypeName = "decimal(15,2)")]
    public decimal TrsValue { get; set; }
    
    /// <summary>
    /// Текущее (накопленное) значение по лимиту
    /// </summary>
    [Column(TypeName = "decimal(15,2)")]
    public decimal UsedValue { get; set; }
    
    /// <summary>
    /// Тип лимита
    /// </summary>
    public LimitType LimitType { get; set; }
}