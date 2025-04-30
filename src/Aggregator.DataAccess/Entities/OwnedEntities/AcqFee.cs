using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Комиссия банка-эквайера в валюте операции
/// </summary>
[Owned]
public class AcqFee : ICurrencyAmount
{
    /// <inheritdoc />
    [Column(TypeName = "decimal(15,2)")]
    public decimal? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}