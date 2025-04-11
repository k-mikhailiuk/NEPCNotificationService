using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.IssFinAuth;

namespace Aggregator.Repositories.Repositories.IssFinAuth;

/// <summary>
/// Репозиторий для работы с IssFinAuthDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IIssFinAuthDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="IssFinAuthDetails"/>.
/// </remarks>
public class IssFinAuthDetailsRepository(AggregatorDbContext context)
    : Repository<IssFinAuthDetails>(context), IIssFinAuthDetailsRepository;