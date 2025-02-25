using System.ComponentModel.DataAnnotations;

namespace Aggregator.DataAccess.Entities.OwiUserAction;

/// <summary>
/// Подробная информация о действии пользователя в OWI
/// </summary>
public class OwiUserActionDetails
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public long OwiUserActionDetailsId { get; set; }
    
    /// <summary>
    /// Время совершения операции в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset TransactionTime { get; set; }
    
    /// <summary>
    /// Login в owi (имя пользователя)
    /// </summary>
    [Required]
    public string Login { get; set; }
    
    /// <summary>
    /// Действие пользователя.
    /// </summary>
    [Required]
    public string Action { get; set; }
}