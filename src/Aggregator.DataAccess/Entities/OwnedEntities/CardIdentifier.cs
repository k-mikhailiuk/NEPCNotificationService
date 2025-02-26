using Aggregator.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Идентификатор карты
/// </summary>
[Owned]
public class CardIdentifier
{
    /// <summary>
    /// Тип идентификатора карты
    /// </summary>
    public CardIdentifierType? CardIdentifierType { get; set; }
    
    /// <summary>
    /// Значение идентификатора карты
    /// </summary>
    public string? CardIdentifierValue { get; set; }
}