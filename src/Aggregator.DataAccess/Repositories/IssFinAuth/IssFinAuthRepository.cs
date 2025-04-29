using Aggregator.DataAccess.Abstractions.Repositories.IssFinAuth;

namespace Aggregator.DataAccess.Repositories.IssFinAuth;

/// <summary>
/// Репозиторий для работы с IssFinAuth.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IIssFinAuthRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="IssFinAuth"/>.
/// </remarks>
public class IssFinAuthRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.IssFinAuth.IssFinAuth>(context), IIssFinAuthRepository;