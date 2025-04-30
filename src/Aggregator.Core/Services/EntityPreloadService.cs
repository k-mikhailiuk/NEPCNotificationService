using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Services;

/// <inheritdoc/>
public class EntityPreloadService(IAggregatorUnitOfWork aggregatorUnitOfWork) : IEntityPreloadService
{
    /// <inheritdoc/>
    public void ProcessEntities<T>(IEnumerable<T> entities) where T : Notification
    {
        var list = entities.ToList();
        if (list.Count == 0) return;

        var ids = list.Select(x => x.NotificationId).ToList();
        
        var existingIds = aggregatorUnitOfWork.Query<T>()
            .Where(x => ids.Contains(x.NotificationId))
            .Select(x => x.NotificationId);
        
        foreach (var e in list.Where(x => !existingIds.Contains(x.NotificationId)))
        {
            aggregatorUnitOfWork.Add(e);
        }
    }
}