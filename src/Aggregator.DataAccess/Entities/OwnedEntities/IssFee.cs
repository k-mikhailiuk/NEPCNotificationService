using System.ComponentModel.DataAnnotations.Schema;
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
    [Column(TypeName = "decimal(15,2)")]
    public decimal? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}