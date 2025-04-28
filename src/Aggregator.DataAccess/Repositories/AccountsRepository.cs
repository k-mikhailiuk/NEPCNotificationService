using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities.ABSEntities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с dbo.Accounts.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAccountsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="Account"/>.
/// </remarks>
public class AccountsRepository(AggregatorDbContext context) : Repository<Account>(context), IAccountsRepository
{
    public IQueryable<Account> GetByAccountNos(List<string?> accountNos)
    {
        if (accountNos == null || accountNos.Count == 0)
            return Enumerable.Empty<Account>().AsQueryable();

        return Context.Set<Account>()
            .Where(a => accountNos.Contains(a.AccountNo));
    }
}

