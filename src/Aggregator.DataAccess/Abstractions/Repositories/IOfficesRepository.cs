using Aggregator.DataAccess.Entities.ABSEntities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

public interface IOfficesRepository : IRepository<Office>
{
    Task<Dictionary<string, int>> GetCustomerIdsByTerminalsAsync(
        IReadOnlyCollection<string> terminalIds,
        CancellationToken cancellationToken);
}