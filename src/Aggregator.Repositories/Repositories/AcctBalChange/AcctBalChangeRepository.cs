using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;

namespace Aggregator.Repositories.Repositories.AcctBalChange;

public class AcctBalChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcctBalChange.AcctBalChange>(context), IAcctBalChangeRepository;