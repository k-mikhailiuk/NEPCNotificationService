using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Доступный баланс
/// </summary>
[Owned]
public class AviableBalance : ICurrencyAmount
{
    /// <inheritdoc />
    public long? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}