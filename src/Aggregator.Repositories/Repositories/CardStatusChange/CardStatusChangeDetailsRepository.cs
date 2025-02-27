using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.Repositories.Abstractions.Repositories.CardStatusChange;

namespace Aggregator.Repositories.Repositories.CardStatusChange;

public class CardStatusChangeDetailsRepository : Repository<CardStatusChangeDetails>, ICardStatusChangeDetailsRepository
{
    public CardStatusChangeDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}