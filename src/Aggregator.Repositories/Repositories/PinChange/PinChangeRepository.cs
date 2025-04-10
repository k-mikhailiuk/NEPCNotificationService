using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;

namespace Aggregator.Repositories.Repositories.PinChange;

public class PinChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.PinChange.PinChange>(context), IPinChangeRepository;