using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.Repositories.Repositories.AcctBalChange;

public class AcctBalChangeRepository : Repository<DataAccess.Entities.AcctBalChange.AcctBalChange>, IAcctBalChangeRepository
{
    public AcctBalChangeRepository(AggregatorDbContext context) : base(context)
    {
    }
}