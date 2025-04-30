using Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;

namespace Aggregator.DataAccess.Repositories.TokenStatusChange;

/// <summary>
/// Репозиторий для работы с TokenStatusChangeDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ITokenStatusChangeDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="TokenStatusChangeDetails"/>.
/// </remarks>
public class TokenStatusChangeDetailsRepository(AggregatorDbContext context)
    : Repository<TokenStatusChangeDetails>(context), ITokenStatusChangeDetailsRepository;