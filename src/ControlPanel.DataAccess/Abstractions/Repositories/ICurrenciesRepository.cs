using ControlPanel.DataAccess.Entites;

namespace ControlPanel.DataAccess.Abstractions.Repositories;

public interface ICurrenciesRepository : IRepository<Currency>
{
    Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken);
}