using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.PinChange;

namespace Aggregator.DataAccess.Repositories.PinChange;

/// <summary>
/// Репозиторий для работы с PinChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IPinChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="PinChange"/>.
/// </remarks>
public class PinChangeRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.PinChange.PinChange>(context), IPinChangeRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.PinChange.PinChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.PinChange.PinChange>(ids, ct);
    }

    /// <inheritdoc/>
    public async Task<List<Entities.PinChange.PinChange>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.PinChange.PinChange, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}