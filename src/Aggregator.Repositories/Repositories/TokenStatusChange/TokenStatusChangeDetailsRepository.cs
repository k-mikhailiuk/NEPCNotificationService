using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.Repositories.Repositories.TokenStatusChange;

public class TokenStatusChangeDetailsRepository : Repository<TokenStatusChangeDetails>, ITokenStatusChangeDetailsRepository
{
    public TokenStatusChangeDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}