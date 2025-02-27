using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class CheckedLimitRepository : Repository<CheckedLimit>, ICheckedLimitRepository
{
    public CheckedLimitRepository(AggregatorDbContext context) : base(context)
    {
    }
}