using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class CardInfoRepository : Repository<CardInfo>, ICardInfoRepository
{
    public CardInfoRepository(AggregatorDbContext context) : base(context)
    {
    }
}