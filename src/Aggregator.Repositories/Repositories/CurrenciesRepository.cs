using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class CurrenciesRepository : Repository<Currency>, ICurrenciesRepository
{
    public CurrenciesRepository(AggregatorDbContext context) : base(context)
    {
    }
    
    public async Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken)
        => await _dbSet.FirstOrDefaultAsync(x => x.CurrencyCode == code, cancellationToken: cancellationToken);
}
