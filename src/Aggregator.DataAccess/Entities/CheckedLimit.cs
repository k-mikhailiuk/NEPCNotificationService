using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Лимит, проверенный при авторизации
/// </summary>
public class CheckedLimit
{
    /// <summary>
    /// Идентификатор лимита
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Тип объекта
    /// </summary>
    [Required]
    public CheckedLimitObjectType ObjectType { get; set; }
    
    /// <summary>
    /// Признак превышения лимита
    /// </summary>
    [Required]
    public bool Exceeded { get; set; }
    
    /// <summary>
    /// Идентификатор IssFinAuthDetails
    /// </summary>
    [Required]
    public long IssFinAuthDetailsId { get; set; }
}