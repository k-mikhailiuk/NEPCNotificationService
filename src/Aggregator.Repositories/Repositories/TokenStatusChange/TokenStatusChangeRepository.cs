using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.Repositories.Repositories.TokenStatusChange;

public class TokenStatusChangeRepository : Repository<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>, ITokenStatusChangeRepository
{
    public TokenStatusChangeRepository(AggregatorDbContext context) : base(context)
    {
    }
}