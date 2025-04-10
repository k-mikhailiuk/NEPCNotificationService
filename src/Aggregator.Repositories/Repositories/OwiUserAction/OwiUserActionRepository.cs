using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.OwiUserAction;

namespace Aggregator.Repositories.Repositories.OwiUserAction;

public class OwiUserActionRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.OwiUserAction.OwiUserAction>(context), IOwiUserActionRepository;