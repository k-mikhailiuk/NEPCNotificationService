using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.Repositories.Abstractions.Repositories.OwiUserAction;

namespace Aggregator.Repositories.Repositories.OwiUserAction;

public class OwiUserActionDetailsRepository : Repository<OwiUserActionDetails>, IOwiUserActionDetailsRepository
{
    public OwiUserActionDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}