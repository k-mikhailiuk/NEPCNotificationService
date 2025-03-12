using NotificationService.Core.Models;

namespace NotificationService.Core.Services.Abstractions;

public interface INotificationBuilder
{
    Task<PushNotificationBaseModel> BuildNotificationAsync();
}