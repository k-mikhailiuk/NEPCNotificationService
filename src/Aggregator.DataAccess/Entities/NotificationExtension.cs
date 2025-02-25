using System.ComponentModel.DataAnnotations;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Расширение
/// </summary>
public class NotificationExtension
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string ExtensionId { get; set; }
    
    [Required]
    public bool Critical { get; set; }
    
    public List<ExtesionParameters> ExtesionParameters { get; set; }
}