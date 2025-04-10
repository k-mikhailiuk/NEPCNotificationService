using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.CardStatusChange;

namespace Aggregator.Repositories.Repositories.CardStatusChange;

public class CardStatusChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.CardStatusChange.CardStatusChange>(context), ICardStatusChangeRepository;