using Aggregator.DTOs.Enums;

namespace Aggregator.DTOs;

/// <summary>
/// Список лимитов, проверенных при авторизации. Не заполняется для отмен
/// </summary>
public record AggregatorCheckedLimitDto
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Тип объекта.
    /// </summary>
    public AggregatorLimitObjectType Type { get; init; }
    
    /// <summary>
    /// Признак превышения лимита
    /// </summary>
    public bool Exceeded { get; init; }
}