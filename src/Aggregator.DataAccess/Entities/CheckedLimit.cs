using System.ComponentModel.DataAnnotations.Schema;
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
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }
    
    /// <summary>
    /// Тип объекта
    /// </summary>
    public CheckedLimitObjectType ObjectType { get; set; }
    
    /// <summary>
    /// Признак превышения лимита
    /// </summary>
    public bool Exceeded { get; set; }
    
    /// <summary>
    /// Идентификатор IssFinAuthDetails
    /// </summary>
    public long IssFinAuthDetailsId { get; set; }
}