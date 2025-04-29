using Aggregator.DataAccess.Abstractions.Repositories.PinChange;

namespace Aggregator.DataAccess.Repositories.PinChange;

/// <summary>
/// Репозиторий для работы с PinChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IPinChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="PinChange"/>.
/// </remarks>
public class PinChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.PinChange.PinChange>(context), IPinChangeRepository;