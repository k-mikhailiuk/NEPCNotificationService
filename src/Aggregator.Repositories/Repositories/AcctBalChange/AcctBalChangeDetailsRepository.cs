using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.Repositories.Repositories.AcctBalChange;

public class AcctBalChangeDetailsRepository : Repository<AcctBalChangeDetails>, IAcctBalChangeDetailsRepository
{
    public AcctBalChangeDetailsRepository(AggregatorDbContext context) : base(context)
    {
    }
}