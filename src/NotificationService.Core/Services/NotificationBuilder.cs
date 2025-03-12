using NotificationService.Core.Models;
using NotificationService.Core.Services.Abstractions;

namespace NotificationService.Core.Services;

public class NotificationBuilder : INotificationBuilder
{
    public async Task<PushNotificationBaseModel> BuildNotificationAsync()
    {
        throw new NotImplementedException();
    }
}