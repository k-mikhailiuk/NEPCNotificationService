using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;

namespace Aggregator.Repositories.Repositories.Unhold;

public class UnholdRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.Unhold.Unhold>(context), IUnholdRepository;