using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

public class Limit
{
    [Key]
    public long LimitId { get; set; }
    
    public string? CycleType { get; set; }
    
    public int? CycleLength { get; set; }
    
    public DateTimeOffset? EndTime { get; set; }
    
    public string? Currency { get; set; }
    
    [Required]
    public long TrsValue { get; set; }
    
    [Required]
    public long UsedValue { get; set; }
    
    [Required]
    public LimitType LimitType { get; set; }
}