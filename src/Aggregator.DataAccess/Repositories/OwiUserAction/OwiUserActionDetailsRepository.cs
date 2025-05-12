using Aggregator.DataAccess.Abstractions.Repositories.OwiUserAction;
using Aggregator.DataAccess.Entities.OwiUserAction;

namespace Aggregator.DataAccess.Repositories.OwiUserAction;

/// <summary>
/// Репозиторий для работы с OwiUserActionDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IOwiUserActionDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="OwiUserActionDetails"/>.
/// </remarks>
public class OwiUserActionDetailsRepository(AggregatorDbContext context)
    : Repository<OwiUserActionDetails>(context), IOwiUserActionDetailsRepository;