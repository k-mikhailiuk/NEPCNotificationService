using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с ExtensionParameter.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IExtensionParameterRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="ExtensionParameter"/>.
/// </remarks>
public class ExtensionParameterRepository(AggregatorDbContext context)
    : Repository<ExtensionParameter>(context), IExtensionParameterRepository;