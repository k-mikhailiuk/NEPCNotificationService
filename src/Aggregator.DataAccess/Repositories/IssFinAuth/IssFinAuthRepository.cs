using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.IssFinAuth;

namespace Aggregator.DataAccess.Repositories.IssFinAuth;

/// <summary>
/// Репозиторий для работы с IssFinAuth.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IIssFinAuthRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="IssFinAuth"/>.
/// </remarks>
public class IssFinAuthRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.IssFinAuth.IssFinAuth>(context), IIssFinAuthRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.IssFinAuth.IssFinAuth>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.IssFinAuth.IssFinAuth>(ids, ct);
    }

    /// <inheritdoc/>
    public async Task<List<Entities.IssFinAuth.IssFinAuth>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.IssFinAuth.IssFinAuth, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}