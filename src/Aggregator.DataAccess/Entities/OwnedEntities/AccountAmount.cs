using System.ComponentModel.DataAnnotations.Schema;
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
    [Column(TypeName = "decimal(15,2)")]
    public decimal? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}