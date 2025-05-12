using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.Abstract;

/// <summary>
/// Базовый класс уведомлений
/// </summary>
public abstract class Notification
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long NotificationId { get; init; }

    /// <summary>
    /// Тип уведомления
    /// </summary>
    public NotificationType NotificationType { get; init; }

    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; init; }

    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset Time { get; init; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; init; }
}