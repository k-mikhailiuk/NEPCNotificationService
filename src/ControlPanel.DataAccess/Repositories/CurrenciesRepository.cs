using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.Repositories;

public class CurrenciesRepository : Repository<Currency>, ICurrenciesRepository
{
    public CurrenciesRepository(ControlPanelDbContext context) : base(context)
    {
    }

    public async Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken)
        => await _dbSet.FirstOrDefaultAsync(x => x.CurrencyCode == code, cancellationToken: cancellationToken);
}