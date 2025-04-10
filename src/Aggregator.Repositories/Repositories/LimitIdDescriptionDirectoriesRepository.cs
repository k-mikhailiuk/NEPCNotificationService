using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class LimitIdDescriptionDirectoriesRepository(AggregatorDbContext context)
    : Repository<LimitIdDescriptionDirectory>(context), ILimitIdDescriptionDirectoriesRepository
{
    public async Task<LimitIdDescriptionDirectory?> GetByLimitCodeAsync(long limitCode, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(x=>x.LimitCode == limitCode, cancellationToken: cancellationToken);
    }
}