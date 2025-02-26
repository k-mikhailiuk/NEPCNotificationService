using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Комиссия банка-эмитента в валюте счета
/// </summary>
[Owned]
public class IssFee : ICurrencyAmount
{
    /// <inheritdoc />
    public long? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}