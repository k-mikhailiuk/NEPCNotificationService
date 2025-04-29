using Aggregator.DataAccess.Entities.ABSEntities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

public interface IPushNotificationSettingsRepository : IRepository<PushNotificationSettings>
{
    Task<Dictionary<int, int>> GetUserSettingsIds(IReadOnlyCollection<int> customerIds,
        CancellationToken cancellationToken);
}