using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.Abstract;

public abstract class Notification
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]

    public long NotificationId { get; set; }

    /// <summary>
    /// Тип уведомления
    /// </summary>
    public NotificationType NotificationType { get; set; }

    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }

    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset Time { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}