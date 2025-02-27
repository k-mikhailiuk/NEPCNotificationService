using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;

namespace Aggregator.Repositories.Repositories.Unhold;

public class UnholdRepository : Repository<DataAccess.Entities.Unhold.Unhold>, IUnholdRepository
{
    public UnholdRepository(AggregatorDbContext context) : base(context)
    {
    }
}