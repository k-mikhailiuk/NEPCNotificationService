using Aggregator.DataAccess.Abstractions.Repositories.OwiUserAction;

namespace Aggregator.DataAccess.Repositories.OwiUserAction;

/// <summary>
/// Репозиторий для работы с OwiUserAction.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IOwiUserActionRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="OwiUserAction"/>.
/// </remarks>
public class OwiUserActionRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.OwiUserAction.OwiUserAction>(context), IOwiUserActionRepository;