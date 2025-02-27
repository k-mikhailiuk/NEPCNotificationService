using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.AcqFinAuth;

namespace Aggregator.Repositories.Repositories.AcqFinAuth;

public class AcqFinAuthDetailsRepository : Repository<AcqFinAuthDetails>, IAcqFinAuthDetailsRepository
{
    public AcqFinAuthDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}