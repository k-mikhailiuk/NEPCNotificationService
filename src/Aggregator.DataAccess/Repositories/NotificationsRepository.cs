using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories;

/// <inheritdoc cref="Aggregator.DataAccess.Abstractions.Repositories.INotificationsRepository" />
public class NotificationsRepository(AggregatorDbContext context)
    : Repository<Notification>(context), INotificationsRepository
{
    
    /// <inheritdoc/>
    public async Task<List<T>> GetListByIdsAsync<T>(
        List<long>? ids,
        CancellationToken cancellationToken = default)
        where T : Notification
    {
        if (ids == null || ids.Count == 0)
            return new List<T>();

        var result = Context.Notifications
            .Where(n => n.NotificationId == ids[0])
            .OfType<T>();

        for (var i = 1; i < ids.Count; i++)
        {
            var id = ids[i];
            result = result.Union(
                Context.Notifications
                    .Where(n => n.NotificationId == id)
                    .OfType<T>()
            );
        }

        return await result.ToListAsync(cancellationToken);
    }
    
    /// <inheritdoc/>
    public async Task<List<T>> GetListByIdsAsync<T>(
        List<long>? ids,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
        where T : Notification
    {
        if (ids == null || ids.Count == 0)
            return new List<T>();

        var result = Context.Notifications
            .Where(n => n.NotificationId == ids[0])
            .OfType<T>();

        result = includes.Aggregate(result, (current, include) => current.Include(include));

        for (var i = 1; i < ids.Count; i++)
        {
            var id = ids[i];
            var next = Context.Notifications
                .Where(n => n.NotificationId == id)
                .OfType<T>();

            next = includes.Aggregate(next, (current, include) => current.Include(include));

            result = result.Union(next);
        }

        return await result.ToListAsync(cancellationToken);
    }
}