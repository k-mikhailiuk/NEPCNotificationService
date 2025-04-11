using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;

namespace Aggregator.Repositories.Repositories.Unhold;

/// <summary>
/// Репозиторий для работы с UnholdDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IUnholdDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="UnholdDetails"/>.
/// </remarks>
public class UnholdDetailsRepository(AggregatorDbContext context)
    : Repository<UnholdDetails>(context), IUnholdDetailsRepository;