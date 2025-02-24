using DataIngrestorApi.DTOs.Enums;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Список лимитов, проверенных при авторизации. Не заполняется для отмен
/// </summary>
public class CheckedLimitDto
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Тип объекта.
    /// </summary>
    public LimitObjectType Type { get; set; }
    
    /// <summary>
    /// Признак превышения лимита
    /// </summary>
    public bool Exceeded { get; set; }
}