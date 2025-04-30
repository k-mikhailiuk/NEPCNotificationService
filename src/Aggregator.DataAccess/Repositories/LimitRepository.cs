using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с Limit.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ILimitRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="Limit"/>.
/// </remarks>
public class LimitRepository(AggregatorDbContext context) : Repository<Limit>(context), ILimitRepository;