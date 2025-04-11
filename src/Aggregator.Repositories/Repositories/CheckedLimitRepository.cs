using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с CheckedLimit.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICheckedLimitRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CheckedLimit"/>.
/// </remarks>
public class CheckedLimitRepository(AggregatorDbContext context)
    : Repository<CheckedLimit>(context), ICheckedLimitRepository;