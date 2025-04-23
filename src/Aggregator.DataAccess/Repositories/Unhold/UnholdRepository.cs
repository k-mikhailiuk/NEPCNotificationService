using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.Unhold;

namespace Aggregator.DataAccess.Repositories.Unhold;

/// <summary>
/// Репозиторий для работы с Unhold.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IUnholdRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="Unhold"/>.
/// </remarks>
public class UnholdRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.Unhold.Unhold>(context), IUnholdRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.Unhold.Unhold>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.Unhold.Unhold>(ids, ct);
    }
    
    /// <inheritdoc/>
    public async Task<List<Entities.Unhold.Unhold>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.Unhold.Unhold, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}