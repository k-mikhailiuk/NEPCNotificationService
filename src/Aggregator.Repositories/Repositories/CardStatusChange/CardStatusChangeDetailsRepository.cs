using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.Repositories.Abstractions.Repositories.CardStatusChange;

namespace Aggregator.Repositories.Repositories.CardStatusChange;

public class CardStatusChangeDetailsRepository(AggregatorDbContext context)
    : Repository<CardStatusChangeDetails>(context), ICardStatusChangeDetailsRepository;