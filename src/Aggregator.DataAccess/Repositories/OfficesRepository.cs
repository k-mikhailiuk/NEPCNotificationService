using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories;

/// <inheritdoc cref="Aggregator.DataAccess.Abstractions.Repositories.IOfficesRepository" />
public class OfficesRepository(AggregatorDbContext context) : Repository<Office>(context), IOfficesRepository
{
    /// <inheritdoc/>
    public async Task<Dictionary<string, int>> GetCustomerIdsByTerminalsAsync(
        IReadOnlyCollection<string> terminalIds,
        CancellationToken cancellationToken)
    {
        if (terminalIds.Count == 0)
            return new Dictionary<string, int>();

        return await Context.Set<Office>()
            .Where(o => terminalIds.Contains(o.DeviceCode))
            .Join(
                Context.Set<Account>(),
                o => o.AccountNoIncome,
                a => a.AccountNo,
                (o, a) => new 
                {
                    TerminalId = o.DeviceCode,
                    CustomerId = a.CustomerID
                }
            )
            .ToDictionaryAsync(
                x => x.TerminalId,
                x => x.CustomerId,
                cancellationToken
            );
    }
}