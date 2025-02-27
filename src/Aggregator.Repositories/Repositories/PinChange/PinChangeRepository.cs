using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;

namespace Aggregator.Repositories.Repositories.PinChange;

public class PinChangeRepository : Repository<DataAccess.Entities.PinChange.PinChange>, IPinChangeRepository
{
    public PinChangeRepository(AggregatorDbContext context) : base(context)
    {
    }
}