using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class AccountsInfoRepository(AggregatorDbContext context)
    : Repository<AccountsInfo>(context), IAccountsInfoRepository;