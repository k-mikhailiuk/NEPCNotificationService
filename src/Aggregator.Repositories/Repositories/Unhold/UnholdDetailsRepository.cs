using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;

namespace Aggregator.Repositories.Repositories.Unhold;

public class UnholdDetailsRepository : Repository<UnholdDetails>, IUnholdDetailsRepository
{
    public UnholdDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}