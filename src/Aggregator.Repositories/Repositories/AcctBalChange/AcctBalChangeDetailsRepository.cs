using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.Repositories.Repositories.AcctBalChange;

public class AcctBalChangeDetailsRepository(AggregatorDbContext context)
    : Repository<AcctBalChangeDetails>(context), IAcctBalChangeDetailsRepository;