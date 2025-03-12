namespace NotificationService.Core.Models;

public class PushNotificationBaseModel
{
    public string MessageBody { get; set; }
    
    public string MessageTittle { get; set; }
    
    public string Destination { get; set; }
}