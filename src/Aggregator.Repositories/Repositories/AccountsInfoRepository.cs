using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class AccountsInfoRepository : Repository<AccountsInfo>, IAccountsInfoRepository
{
    public AccountsInfoRepository(AggregatorDbContext context) : base(context)
    {
    }
}