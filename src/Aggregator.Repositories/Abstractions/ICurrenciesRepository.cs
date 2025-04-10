using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions;

public interface ICurrenciesRepository : IRepository<Currency>
{
    Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken);
}