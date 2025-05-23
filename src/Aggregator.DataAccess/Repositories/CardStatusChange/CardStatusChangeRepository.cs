using Aggregator.DataAccess.Abstractions.Repositories.CardStatusChange;

namespace Aggregator.DataAccess.Repositories.CardStatusChange;

/// <summary>
/// Репозиторий для работы с CardStatusChange.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardStatusChangeRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardStatusChange"/>.
/// </remarks>
public class CardStatusChangeRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.CardStatusChange.CardStatusChange>(context), ICardStatusChangeRepository;
