using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Сумма авторизации в валюте счета. Не включает эмитентскую комиссию
/// </summary>
[Owned]
public class UnholdMoney : ICurrencyAmount
{
    /// <inheritdoc />
    public long Amount { get; set; }
    
    /// <inheritdoc />
    public string Currency { get; set; }
}