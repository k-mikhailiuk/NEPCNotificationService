using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

public class AccountsInfoLimitWrapper
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public LimitType LimitType { get; set; }
    
    [Required]
    public long CardInfoId { get; set; }
    
    [Required]
    public long LimitId { get; set; }
    
    [ForeignKey(nameof(LimitId))]
    public Limit Limit { get; set; }
}