using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class AccountsInfoLimitWrapperRepository(AggregatorDbContext context)
    : Repository<AccountsInfoLimitWrapper>(context), IAccountsInfoLimitWrapperRepository;