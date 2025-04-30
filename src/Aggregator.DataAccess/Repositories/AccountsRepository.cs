using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;

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
    public async Task<Dictionary<string, int>> GetAccountCustomerMapAsync(IReadOnlyCollection<string> accountNos,
        CancellationToken cancellationToken)
    {
        if (accountNos.Count == 0)
            return new Dictionary<string, int>();

        return await Context.Set<Account>()
            .Where(a => accountNos.Contains(a.AccountNo))
            .ToDictionaryAsync(
                a => a.AccountNo,
                a => a.CustomerID,
                cancellationToken);
    }
}