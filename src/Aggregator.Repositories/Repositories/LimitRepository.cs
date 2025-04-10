using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class LimitRepository(AggregatorDbContext context) : Repository<Limit>(context), ILimitRepository;