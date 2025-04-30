using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с CheckedLimit.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICheckedLimitRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CheckedLimit"/>.
/// </remarks>
public class CheckedLimitRepository(AggregatorDbContext context)
    : Repository<CheckedLimit>(context), ICheckedLimitRepository;