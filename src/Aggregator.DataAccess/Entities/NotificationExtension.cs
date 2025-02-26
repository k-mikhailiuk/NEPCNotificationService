using System.ComponentModel.DataAnnotations;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Расширение
/// </summary>
public class NotificationExtension
{
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    [Required]
    public string ExtensionId { get; set; }
    
    /// <summary>
    /// Признак критичности расширения (0 - false, 1 - true)
    /// </summary>
    [Required]
    public bool Critical { get; set; }
    
    /// <summary>
    /// Список параметров расширения
    /// </summary>
    public List<ExtesionParameters> ExtesionParameters { get; set; }
}