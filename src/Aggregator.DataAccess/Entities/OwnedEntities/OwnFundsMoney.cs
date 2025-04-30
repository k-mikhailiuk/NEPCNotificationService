using System.ComponentModel.DataAnnotations.Schema;
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
    [Column(TypeName = "decimal(15,2)")]
    public decimal? Amount { get; set; }
    
    /// <inheritdoc />
    public string? Currency { get; set; }
}