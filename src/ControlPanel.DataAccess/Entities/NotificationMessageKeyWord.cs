using System.ComponentModel.DataAnnotations.Schema;
using ControlPanel.DataAccess.Entites.Enum;

namespace ControlPanel.DataAccess.Entities;

public class NotificationMessageKeyWord
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    public string KeyWord { get; set; }
    
    public string? Description { get; set; }
    
    public NotificationMessageType NotificationType { get; set; }
    
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