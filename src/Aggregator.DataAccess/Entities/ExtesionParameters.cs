using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities;

public class ExtesionParameters
{
    
    [Key]
    public long Id { get; set; }
    
    [Required]
    public long ExtensionId { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    [ForeignKey(nameof(ExtensionId))]
    public NotificationExtension Extension { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Value { get; set; }
}