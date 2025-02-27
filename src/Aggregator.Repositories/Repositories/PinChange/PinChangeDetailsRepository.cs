using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;

namespace Aggregator.Repositories.Repositories.PinChange;

public class PinChangeDetailsRepository : Repository<PinChangeDetails>, IPinChangeDetailsRepository
{
    public PinChangeDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}