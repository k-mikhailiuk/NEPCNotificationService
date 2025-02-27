using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class AccountsInfoLimitWrapperRepository : Repository<AccountsInfoLimitWrapper>, IAccountsInfoLimitWrapperRepository
{
    public AccountsInfoLimitWrapperRepository(AggregatorDbContext context) : base(context)
    {
    }
}