using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class LimitIdDescriptionDirectoriesRepository : Repository<LimitIdDescriptionDirectory>, ILimitIdDescriptionDirectoriesRepository
{
    public LimitIdDescriptionDirectoriesRepository(AggregatorDbContext context) : base(context)
    {
    }

    public async Task<LimitIdDescriptionDirectory?> GetByLimitCodeAsync(long limitCode, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(x=>x.LimitCode == limitCode, cancellationToken: cancellationToken);
    }
}