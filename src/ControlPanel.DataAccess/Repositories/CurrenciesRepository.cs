using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.Repositories;

public class CurrenciesRepository(ControlPanelDbContext context) : Repository<Currency>(context), ICurrenciesRepository
{
    public async Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken)
        => await _dbSet.FirstOrDefaultAsync(x => x.CurrencyCode == code, cancellationToken: cancellationToken);
}