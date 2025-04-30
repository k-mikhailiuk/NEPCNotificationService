using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Core.Services.Abstractions;

public interface INotificationCompositor<T> where T : Notification
{
    Task<List<NotificationMessage>> ComposeAsync(IEnumerable<T> messages,
        Dictionary<long, NotificationMessageTextDirectory> notificationTextById,
        Dictionary<long, int> notificationToCustomer,
        Dictionary<int, int> customerSettingsMap,
        CancellationToken cancellationToken);
}