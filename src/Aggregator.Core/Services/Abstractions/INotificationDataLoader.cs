using Aggregator.Core.Models;
using Aggregator.DataAccess.Abstractions;

namespace Aggregator.Core.Services.Abstractions;

public interface INotificationDataLoader<T>
{
    Task<NotificationDataLoad<T>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken);
}