using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.OwiUserAction;

namespace Aggregator.DataAccess.Repositories.OwiUserAction;

/// <summary>
/// Репозиторий для работы с OwiUserAction.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IOwiUserActionRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="OwiUserAction"/>.
/// </remarks>
public class OwiUserActionRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.OwiUserAction.OwiUserAction>(context), IOwiUserActionRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.OwiUserAction.OwiUserAction>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.OwiUserAction.OwiUserAction>(ids, ct);
    }
    
    /// <inheritdoc/>
    public async Task<List<Entities.OwiUserAction.OwiUserAction>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.OwiUserAction.OwiUserAction, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}