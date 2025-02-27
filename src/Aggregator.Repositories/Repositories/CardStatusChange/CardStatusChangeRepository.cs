using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.CardStatusChange;

namespace Aggregator.Repositories.Repositories.CardStatusChange;

public class CardStatusChangeRepository : Repository<DataAccess.Entities.CardStatusChange.CardStatusChange>, ICardStatusChangeRepository
{
    public CardStatusChangeRepository(AggregatorDbContext context) : base(context)
    {
    }
}