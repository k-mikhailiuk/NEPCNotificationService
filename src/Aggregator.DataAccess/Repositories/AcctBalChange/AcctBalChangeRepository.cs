using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.DataAccess.Repositories.AcctBalChange;

/// <summary>
/// Репозиторий для работы с AcctBalChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcctBalChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcctBalChange"/>.
/// </remarks>
public class AcctBalChangeRepository(AggregatorDbContext context, INotificationsRepository notifications)
    : Repository<DataAccess.Entities.AcctBalChange.AcctBalChange>(context), IAcctBalChangeRepository
{
    /// <inheritdoc/>
    public Task<List<Entities.AcctBalChange.AcctBalChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default)
    {
        return notifications.GetListByIdsAsync<Entities.AcctBalChange.AcctBalChange>(ids, ct);
    }

    /// <inheritdoc/>
    public async Task<List<Entities.AcctBalChange.AcctBalChange>> GetByIdsWithIncludesAsync(List<long> ids, CancellationToken ct = default, params Expression<Func<Entities.AcctBalChange.AcctBalChange, object>>[] includes)
    {
        return await notifications.GetListByIdsAsync(ids, ct, includes);
    }
}
    
