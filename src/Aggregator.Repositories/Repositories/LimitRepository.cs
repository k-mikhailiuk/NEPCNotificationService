using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class LimitRepository : Repository<Limit>, ILimitRepository
{
    public LimitRepository(AggregatorDbContext context) : base(context)
    {
    }
}