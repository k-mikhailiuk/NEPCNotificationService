using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

public class CheckedLimit
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public CheckedLimitObjectType ObjectType { get; set; }
    
    [Required]
    public bool Exceeded { get; set; }
    
    [Required]
    public long IssFinAuthDetailsId { get; set; }
}