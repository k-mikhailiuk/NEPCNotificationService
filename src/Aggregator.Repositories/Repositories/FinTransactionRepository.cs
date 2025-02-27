using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class FinTransactionRepository : Repository<FinTransaction>, IFinTransactionRepository
{
    public FinTransactionRepository(AggregatorDbContext context) : base(context)
    {
    }
}