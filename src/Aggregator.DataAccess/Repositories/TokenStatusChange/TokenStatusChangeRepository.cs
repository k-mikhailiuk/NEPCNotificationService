using Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.DataAccess.Repositories.TokenStatusChange;

/// <summary>
/// Репозиторий для работы с TokenStatusChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ITokenStatusChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="TokenStatusChange"/>.
/// </remarks>
public class TokenStatusChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>(context), ITokenStatusChangeRepository;