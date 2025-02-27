using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.OwiUserAction;

namespace Aggregator.Repositories.Repositories.OwiUserAction;

public class OwiUserActionRepository : Repository<DataAccess.Entities.OwiUserAction.OwiUserAction>, IOwiUserActionRepository
{
    public OwiUserActionRepository(AggregatorDbContext context) : base(context)
    {
    }
}