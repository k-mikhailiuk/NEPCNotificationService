using DataIngrestorApi.DTOs.Enums;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Список лимитов, проверенных при авторизации. Не заполняется для отмен
/// </summary>
public record CheckedLimitDto
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Тип объекта.
    /// </summary>
    public LimitObjectType Type { get; init; }
    
    /// <summary>
    /// Признак превышения лимита
    /// </summary>
    public bool Exceeded { get; init; }
}