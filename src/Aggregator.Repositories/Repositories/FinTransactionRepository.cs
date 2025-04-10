using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class FinTransactionRepository(AggregatorDbContext context)
    : Repository<FinTransaction>(context), IFinTransactionRepository;