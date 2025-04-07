using System.ComponentModel.DataAnnotations.Schema;
using ControlPanel.DataAccess.Entites.Enum;

namespace ControlPanel.DataAccess.Entities;

public class NotificationMessageTextDirectory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public NotificationMessageType NotificationType { get; set; }

    public NotificationOperationType? OperationType { get; set; }

    public string? MessageTextRu { get; set; }
    public string? MessageTextEn { get; set; }
    public string? MessageTextKg { get; set; }
    
    public bool IsNeedSend { get; set; }

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