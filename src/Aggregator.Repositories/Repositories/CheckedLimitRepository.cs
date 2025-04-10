using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class CheckedLimitRepository(AggregatorDbContext context)
    : Repository<CheckedLimit>(context), ICheckedLimitRepository;