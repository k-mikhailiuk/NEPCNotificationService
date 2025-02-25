using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Сумма транзакции в валюте счета
/// </summary>
[Owned]
public class TranMoney : ICurrencyAmount
{
    /// <inheritdoc />
    public long Amount { get; set; }
    
    /// <inheritdoc />
    public string Currency { get; set; }
}