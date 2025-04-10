using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;

namespace Aggregator.Repositories.Repositories.PinChange;

public class PinChangeDetailsRepository(AggregatorDbContext context)
    : Repository<PinChangeDetails>(context), IPinChangeDetailsRepository;