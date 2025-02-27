using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class MerchantInfoRepository : Repository<MerchantInfo>, IMerchantInfoRepository
{
    public MerchantInfoRepository(AggregatorDbContext context) : base(context)
    {
    }
}