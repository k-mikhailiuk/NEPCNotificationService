using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.AcqFinAuth;

namespace Aggregator.DataAccess.Repositories.AcqFinAuth;

/// <summary>
/// Репозиторий для работы с AcqFinAuth.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcqFinAuthRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcqFinAuth"/>.
/// </remarks>
public class AcqFinAuthRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.AcqFinAuth.AcqFinAuth>(context), IAcqFinAuthRepository
{
    /// <inheritdoc/>
    public Task<List<DataAccess.Entities.AcqFinAuth.AcqFinAuth>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<DataAccess.Entities.AcqFinAuth.AcqFinAuth>(ids, ct);
    }

    /// <inheritdoc/>
    public async Task<List<Entities.AcqFinAuth.AcqFinAuth>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.AcqFinAuth.AcqFinAuth, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}