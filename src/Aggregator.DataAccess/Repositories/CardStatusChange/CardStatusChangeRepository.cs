using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.CardStatusChange;

namespace Aggregator.DataAccess.Repositories.CardStatusChange;

/// <summary>
/// Репозиторий для работы с CardStatusChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardStatusChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardStatusChange"/>.
/// </remarks>
public class CardStatusChangeRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.CardStatusChange.CardStatusChange>(context), ICardStatusChangeRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.CardStatusChange.CardStatusChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.CardStatusChange.CardStatusChange>(ids, ct);
    }

    /// <inheritdoc/>
    public async Task<List<Entities.CardStatusChange.CardStatusChange>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.CardStatusChange.CardStatusChange, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}