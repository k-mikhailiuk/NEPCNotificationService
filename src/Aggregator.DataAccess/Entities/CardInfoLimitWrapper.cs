using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Класс для хранения лимитов, связанных с информацией о картах.
/// </summary>
public class CardInfoLimitWrapper
{
    /// <summary>
    /// Уникальный идентификатор записи.
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Тип лимита.
    /// </summary>
    [Required]
    public LimitType LimitType { get; set; }
    
    /// <summary>
    /// Идентификатор CardInfoId.
    /// </summary>
    [Required]
    public long CardInfoId { get; set; }
    
    /// <summary>
    /// Идентификатор лимита.
    /// </summary>
    [Required]
    public long LimitId { get; set; }
    
    /// <inheritdoc cref="Limit" />
    [ForeignKey(nameof(LimitId))]
    public Limit Limit { get; set; }
}