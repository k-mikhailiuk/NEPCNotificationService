using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Состояние счета после операции
/// </summary>
[Owned]
public class AccountBalance : ICurrencyAmount
{
    /// <inheritdoc />
    public long Amount { get; set; }
    
    /// <inheritdoc />
    public string Currency { get; set; }
}