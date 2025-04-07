using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Repositories;

public class LimitIdDescriptionDirectoriesRepository : Repository<LimitIdDescriptionDirectory>, ILimitIdDescriptionDirectoriesRepository
{
    public LimitIdDescriptionDirectoriesRepository(AggregatorDbContext context) : base(context)
    {
    }
}