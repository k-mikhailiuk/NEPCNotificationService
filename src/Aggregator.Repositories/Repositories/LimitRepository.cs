using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с Limit.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ILimitRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="Limit"/>.
/// </remarks>
public class LimitRepository(AggregatorDbContext context) : Repository<Limit>(context), ILimitRepository;