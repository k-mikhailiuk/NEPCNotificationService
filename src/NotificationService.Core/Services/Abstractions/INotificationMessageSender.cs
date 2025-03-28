using Aggregator.DataAccess.Entities;

namespace NotificationService.Core.Services.Abstractions;

public interface INotificationMessageSender
{
    Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default);
}