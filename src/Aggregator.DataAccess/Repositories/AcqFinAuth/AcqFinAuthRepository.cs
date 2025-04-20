using Aggregator.DataAccess.Abstractions.Repositories.AcqFinAuth;

namespace Aggregator.DataAccess.Repositories.AcqFinAuth;

/// <summary>
/// Репозиторий для работы с AcqFinAuth.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcqFinAuthRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcqFinAuth"/>.
/// </remarks>
public class AcqFinAuthRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcqFinAuth.AcqFinAuth>(context), IAcqFinAuthRepository;