using Aggregator.DataAccess.Abstractions.Repositories.IssFinAuth;
using Aggregator.DataAccess.Entities.IssFinAuth;

namespace Aggregator.DataAccess.Repositories.IssFinAuth;

/// <summary>
/// Репозиторий для работы с IssFinAuthDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IIssFinAuthDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="IssFinAuthDetails"/>.
/// </remarks>
public class IssFinAuthDetailsRepository(AggregatorDbContext context)
    : Repository<IssFinAuthDetails>(context), IIssFinAuthDetailsRepository;