using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class CardInfoLimitWrapperRepository : Repository<CardInfoLimitWrapper>, ICardInfoLimitWrapperRepository
{
    public CardInfoLimitWrapperRepository(AggregatorDbContext context) : base(context)
    {
    }
}