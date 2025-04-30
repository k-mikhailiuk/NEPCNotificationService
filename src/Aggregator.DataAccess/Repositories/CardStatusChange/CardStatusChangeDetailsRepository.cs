using Aggregator.DataAccess.Abstractions.Repositories.CardStatusChange;
using Aggregator.DataAccess.Entities.CardStatusChange;

namespace Aggregator.DataAccess.Repositories.CardStatusChange;

/// <summary>
/// Репозиторий для работы с CardStatusChangeDetails.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardStatusChangeDetailsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardStatusChangeDetails"/>.
/// </remarks>
public class CardStatusChangeDetailsRepository(AggregatorDbContext context)
    : Repository<CardStatusChangeDetails>(context), ICardStatusChangeDetailsRepository;