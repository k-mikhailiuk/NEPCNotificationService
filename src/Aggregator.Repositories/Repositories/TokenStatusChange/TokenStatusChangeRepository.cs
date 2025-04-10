using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.Repositories.Repositories.TokenStatusChange;

public class TokenStatusChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>(context), ITokenStatusChangeRepository;