using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.Repositories.Repositories.TokenStatusChange;

public class TokenStatusChangeDetailsRepository(AggregatorDbContext context)
    : Repository<TokenStatusChangeDetails>(context), ITokenStatusChangeDetailsRepository;