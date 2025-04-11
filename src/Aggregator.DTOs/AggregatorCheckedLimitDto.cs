using Aggregator.DTOs.Enums;

namespace Aggregator.DTOs;

/// <summary>
/// Список лимитов, проверенных при авторизации. Не заполняется для отмен
/// </summary>
public class AggregatorCheckedLimitDto
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Тип объекта.
    /// </summary>
    public AggregatorLimitObjectType Type { get; set; }
    
    /// <summary>
    /// Признак превышения лимита
    /// </summary>
    public bool Exceeded { get; set; }
}