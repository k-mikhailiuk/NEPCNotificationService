using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Расширение
/// </summary>
public class NotificationExtension
{
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    public string ExtensionId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long NotificationId { get; set; }
    
    /// <inheritdoc cref="NotificationType" />
    public NotificationType NotificationType { get; set; }
    
    /// <summary>
    /// Признак критичности расширения (0 - false, 1 - true)
    /// </summary>
    public bool Critical { get; set; }
    
    /// <summary>
    /// Список параметров расширения
    /// </summary>
    public List<ExtensionParameter>? ExtesionParameters { get; set; }
}