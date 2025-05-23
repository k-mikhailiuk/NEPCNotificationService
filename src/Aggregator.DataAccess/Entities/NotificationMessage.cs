using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Сообщение для пользователя
/// </summary>
public class NotificationMessage
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Заголовок уведомления
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Сообщения уведомления
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Статус
    /// </summary>
    public NotificationMessageStatus Status { get; set; }
    
    /// <summary>
    /// Id клиента АБС
    /// </summary>
    public long CustomerId { get; set; }
}