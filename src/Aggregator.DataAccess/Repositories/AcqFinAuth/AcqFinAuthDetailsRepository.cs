using Aggregator.DataAccess.Abstractions.Repositories.AcqFinAuth;
using Aggregator.DataAccess.Entities.AcqFinAuth;

namespace Aggregator.DataAccess.Repositories.AcqFinAuth;

/// <summary>
/// Репозиторий для работы с AcqFinAuthDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IAcqFinAuthDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="AcqFinAuthDetails"/>.
/// </remarks>
public class AcqFinAuthDetailsRepository(AggregatorDbContext context)
    : Repository<AcqFinAuthDetails>(context), IAcqFinAuthDetailsRepository;