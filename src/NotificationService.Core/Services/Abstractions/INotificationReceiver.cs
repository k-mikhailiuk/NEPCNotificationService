namespace NotificationService.Core.Services.Abstractions;

public interface INotificationReceiver
{
    Task GetNotificationsAsync();
}