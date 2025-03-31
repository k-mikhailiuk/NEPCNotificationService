using Aggregator.DataAccess.Entities;

namespace NotificationService.Core.Services.Abstractions;

public interface INotificationHistorySaver
{
    Task SaveAsync(NotificationMessage message, CancellationToken cancellationToken = default);
}