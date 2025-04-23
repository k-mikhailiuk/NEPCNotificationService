using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.DataAccess.Repositories.TokenStatusChange;

/// <summary>
/// Репозиторий для работы с TokenStatusChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ITokenStatusChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="TokenStatusChange"/>.
/// </remarks>
public class TokenStatusChangeRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>(context), ITokenStatusChangeRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>(ids, ct);
    }

    /// <inheritdoc/>
    public async Task<List<Entities.TokenChangeStatus.TokenStatusChange>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.TokenChangeStatus.TokenStatusChange, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}