using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Сумма изменения в валюте счёта, включая комиссии
/// </summary>
[Owned]
public class AccountAmount : ICurrencyAmount
{
    /// <inheritdoc />
    public long? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}