using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Services.Abstractions;

public interface INotificationMessageBuilder<out T> where T : class, INotification
{
    Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds, CancellationToken cancellationToken);
}