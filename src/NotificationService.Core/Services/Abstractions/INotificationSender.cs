using NotificationService.Core.Models;

namespace NotificationService.Core.Services.Abstractions;

public interface INotificationSender
{
    Task SendNotificationAsync(PushNotificationBaseModel notification);
}