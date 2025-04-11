using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;

namespace Aggregator.Repositories.Repositories.TokenStatusChange;

/// <summary>
/// Репозиторий для работы с TokenStatusChangeDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ITokenStatusChangeDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="TokenStatusChangeDetails"/>.
/// </remarks>
public class TokenStatusChangeDetailsRepository(AggregatorDbContext context)
    : Repository<TokenStatusChangeDetails>(context), ITokenStatusChangeDetailsRepository;