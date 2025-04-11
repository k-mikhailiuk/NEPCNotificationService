using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;

namespace Aggregator.Repositories.Repositories.PinChange;

/// <summary>
/// Репозиторий для работы с PinChangeDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IPinChangeDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="PinChangeDetails"/>.
/// </remarks>
public class PinChangeDetailsRepository(AggregatorDbContext context)
    : Repository<PinChangeDetails>(context), IPinChangeDetailsRepository;