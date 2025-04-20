using Aggregator.DataAccess.Abstractions.Repositories.PinChange;
using Aggregator.DataAccess.Entities.PinChange;

namespace Aggregator.DataAccess.Repositories.PinChange;

/// <summary>
/// Репозиторий для работы с PinChangeDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IPinChangeDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="PinChangeDetails"/>.
/// </remarks>
public class PinChangeDetailsRepository(AggregatorDbContext context)
    : Repository<PinChangeDetails>(context), IPinChangeDetailsRepository;