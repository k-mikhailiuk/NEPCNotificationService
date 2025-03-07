using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.Abstract;

public interface INotification
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
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
}