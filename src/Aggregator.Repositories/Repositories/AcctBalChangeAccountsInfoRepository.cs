using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class AcctBalChangeAccountsInfoRepository : Repository<AcctBalChangeAccountsInfo>, IAcctBalChangeAccountsInfoRepository
{
    public AcctBalChangeAccountsInfoRepository(AggregatorDbContext context) : base(context)
    {
    }
}