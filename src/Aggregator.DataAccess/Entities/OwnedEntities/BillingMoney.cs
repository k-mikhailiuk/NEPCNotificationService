using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Сумма авторизации в валюте банка-эмитента. Включает эквайринговую комиссию. Не включает эмитентскую комиссию
/// </summary>
[Owned]
public class BillingMoney : ICurrencyAmount
{
    /// <inheritdoc />
    public long Amount { get; set; }
    
    /// <inheritdoc />
    public string Currency { get; set; }
}