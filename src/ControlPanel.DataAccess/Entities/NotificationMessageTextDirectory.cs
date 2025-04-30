using System.ComponentModel.DataAnnotations.Schema;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.Entities;

/// <summary>
/// Представляет справочник текстов уведомлений, содержащий информацию о типе уведомления, типе операции, текстах на разных языках и флаг необходимости отправки.
/// </summary>
public class NotificationMessageTextDirectory
{
    /// <summary>
    /// Уникальный идентификатор записи справочника. Значение генерируется автоматически базой данных.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Тип уведомления, к которому относится данный текст.
    /// </summary>
    public NotificationMessageType NotificationType { get; set; }

    /// <summary>
    /// Тип операции уведомления (необязательное поле).
    /// </summary>
    public NotificationOperationType? OperationType { get; set; }

    /// <summary>
    /// Текст уведомления на русском языке.
    /// </summary>
    [Unicode]
    public string? MessageTextRu { get; set; }
    
    /// <summary>
    /// Текст уведомления на английском языке.
    /// </summary>
    [Unicode]
    public string? MessageTextEn { get; set; }
    
    /// <summary>
    /// Текст уведомления на кыргызском языке.
    /// </summary>
    [Unicode]
    public string? MessageTextKg { get; set; }
    
    /// <summary>
    /// Флаг, указывающий, нужно ли отправлять уведомление.
    /// </summary>
    public bool IsNeedSend { get; set; }

    /// <summary>
    /// Создает новый экземпляр <see cref="NotificationMessageTextDirectory"/> с заданным типом уведомления и опциональным типом операции.
    /// Изначально флаг отправки уведомления (IsNeedSend) устанавливается в значение false.
    /// </summary>
    /// <param name="type">Тип уведомления. Не должен иметь значение <see cref="NotificationMessageType.Undefined"/>.</param>
    /// <param name="operationType">Опциональный тип операции уведомления.</param>
    /// <returns>Новый экземпляр <see cref="NotificationMessageTextDirectory"/> с заданными параметрами.</returns>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если <paramref name="type"/> имеет значение <see cref="NotificationMessageType.Undefined"/>.
    /// </exception>
    public static NotificationMessageTextDirectory Create(NotificationMessageType type,
        NotificationOperationType? operationType = null)
    {
        if (type == NotificationMessageType.Undefined)
            throw new ArgumentException("NotificationType cannot be Undefined.", nameof(type));

        return new NotificationMessageTextDirectory
        {
            NotificationType = type,
            OperationType = operationType,
            IsNeedSend = false
        };
    }
}