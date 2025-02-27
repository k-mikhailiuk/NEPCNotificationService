using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class IssFinAuthAccountsInfoRepository : Repository<IssFinAuthAccountsInfo>, IIssFinAuthAccountsInfoRepository
{
    public IssFinAuthAccountsInfoRepository(AggregatorDbContext context) : base(context)
    {
    }
}