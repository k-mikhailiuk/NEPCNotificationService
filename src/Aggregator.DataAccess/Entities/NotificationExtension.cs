using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Расширение
/// </summary>
public class NotificationExtension
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    [Required]
    public string ExtensionId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [Required]
    public long NotificationId { get; set; }
    
    /// <inheritdoc cref="NotificationType" />
    [Required]
    public NotificationType NotificationType { get; set; }
    
    /// <summary>
    /// Признак критичности расширения (0 - false, 1 - true)
    /// </summary>
    [Required]
    public bool Critical { get; set; }
    
    /// <summary>
    /// Список параметров расширения
    /// </summary>
    public List<ExtensionParameter> ExtesionParameters { get; set; }
}