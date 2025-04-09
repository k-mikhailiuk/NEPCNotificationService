using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

public interface ILimitIdDescriptionDirectoriesRepository : IRepository<LimitIdDescriptionDirectory>
{
    Task<LimitIdDescriptionDirectory?> GetByLimitCodeAsync(long limitCode, CancellationToken cancellationToken);
}