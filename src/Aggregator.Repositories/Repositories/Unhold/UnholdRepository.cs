using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;

namespace Aggregator.Repositories.Repositories.Unhold;

/// <summary>
/// Репозиторий для работы с Unhold.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IUnholdRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="Unhold"/>.
/// </remarks>
public class UnholdRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.Unhold.Unhold>(context), IUnholdRepository;