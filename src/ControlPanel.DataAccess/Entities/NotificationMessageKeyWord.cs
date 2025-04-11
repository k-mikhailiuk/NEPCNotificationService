using System.ComponentModel.DataAnnotations.Schema;
using ControlPanel.DataAccess.Entities.Enum;

namespace ControlPanel.DataAccess.Entities;

/// <summary>
/// Представляет ключевое слово уведомления с описанием и типом уведомления.
/// </summary>
public class NotificationMessageKeyWord
{
    /// <summary>
    /// Уникальный идентификатор записи. Значение генерируется автоматически базой данных.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    /// <summary>
    /// Ключевое слово уведомления.
    /// </summary>
    public string KeyWord { get; set; }
    
    /// <summary>
    /// Дополнительное описание ключевого слова (необязательное).
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Тип уведомления, к которому относится данное ключевое слово.
    /// </summary>
    public NotificationMessageType NotificationType { get; set; }
    
    /// <summary>
    /// Создает новый экземпляр <see cref="NotificationMessageKeyWord"/> с заданными параметрами.
    /// </summary>
    /// <param name="keyWord">Ключевое слово уведомления. Не должно быть пустым или состоять только из пробелов.</param>
    /// <param name="type">Тип уведомления. Не может быть равным <see cref="NotificationMessageType.Undefined"/>.</param>
    /// <param name="description">Необязательное описание ключевого слова уведомления.</param>
    /// <returns>Новый экземпляр <see cref="NotificationMessageKeyWord"/> с заданными параметрами.</returns>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если <paramref name="keyWord"/> пустое или состоит только из пробелов, либо если <paramref name="type"/> имеет значение <see cref="NotificationMessageType.Undefined"/>.
    /// </exception>
    public static NotificationMessageKeyWord Create(string keyWord, NotificationMessageType type, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(keyWord))
            throw new ArgumentException("KeyWord cannot be null or whitespace.", nameof(keyWord));
        
        if (type == NotificationMessageType.Undefined)
            throw new ArgumentException("NotificationType cannot be Undefined.", nameof(type));
        
        return new NotificationMessageKeyWord
        {
            KeyWord = keyWord,
            Description = description,
            NotificationType = type
        }; 
    }
}