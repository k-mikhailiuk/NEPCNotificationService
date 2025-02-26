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
    public long Id { get; set; }
    
    /// <summary>
    /// Тип лимита.
    /// </summary>
    public LimitType LimitType { get; set; }
    
    /// <summary>
    /// Идентификатор CardInfoId.
    /// </summary>
    public long CardInfoId { get; set; }
    
    /// <summary>
    /// Идентификатор лимита.
    /// </summary>
    public long LimitId { get; set; }
    
    /// <inheritdoc cref="Limit" />
    public Limit Limit { get; set; }
}