using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class MerchantInfoRepository(AggregatorDbContext context)
    : Repository<MerchantInfo>(context), IMerchantInfoRepository;