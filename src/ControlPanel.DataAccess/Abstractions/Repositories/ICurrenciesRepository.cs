using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Abstractions.Repositories;

public interface ICurrenciesRepository : IRepository<Currency>
{
    Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken);
}