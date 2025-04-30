using Aggregator.DataAccess.Abstractions.Repositories.Unhold;
using Aggregator.DataAccess.Entities.Unhold;

namespace Aggregator.DataAccess.Repositories.Unhold;

/// <summary>
/// Репозиторий для работы с UnholdDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IUnholdDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="UnholdDetails"/>.
/// </remarks>
public class UnholdDetailsRepository(AggregatorDbContext context)
    : Repository<UnholdDetails>(context), IUnholdDetailsRepository;