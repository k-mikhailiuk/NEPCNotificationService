using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Сумма(со знаком) изменения собственных средств
/// </summary>
[Owned]
public class OwnFundsMoney: ICurrencyAmount
{
    /// <inheritdoc />
    public long? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}