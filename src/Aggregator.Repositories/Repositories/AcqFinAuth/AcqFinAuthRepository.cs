using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcqFinAuth;

namespace Aggregator.Repositories.Repositories.AcqFinAuth;

public class AcqFinAuthRepository : Repository<DataAccess.Entities.AcqFinAuth.AcqFinAuth>, IAcqFinAuthRepository
{
    public AcqFinAuthRepository(AggregatorDbContext context) : base(context)
    {
    }
}