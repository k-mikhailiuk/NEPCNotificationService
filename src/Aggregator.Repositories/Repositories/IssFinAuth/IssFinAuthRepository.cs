using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.IssFinAuth;

namespace Aggregator.Repositories.Repositories.IssFinAuth;

public class IssFinAuthRepository : Repository<DataAccess.Entities.IssFinAuth.IssFinAuth>, IIssFinAuthRepository
{
    public IssFinAuthRepository(AggregatorDbContext context) : base(context)
    {
    }
}