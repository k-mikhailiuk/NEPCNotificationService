using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.Repositories.Abstractions.Repositories.OwiUserAction;

namespace Aggregator.Repositories.Repositories.OwiUserAction;

/// <summary>
/// Репозиторий для работы с OwiUserActionDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IOwiUserActionDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="OwiUserActionDetails"/>.
/// </remarks>
public class OwiUserActionDetailsRepository(AggregatorDbContext context)
    : Repository<OwiUserActionDetails>(context), IOwiUserActionDetailsRepository;