using Aggregator.Core.Models;
using Aggregator.DataAccess.Abstractions;
using ControlPanel.DataAccess.Abstractions;

namespace Aggregator.Core.Services.Abstractions;

public interface INotificationDataLoader<T>
{
    Task<NotificationDataLoad<T>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork, IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken);
}