using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с CardInfoLimitWrapper.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardInfoLimitWrapperRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardInfoLimitWrapper"/>.
/// </remarks>
public class CardInfoLimitWrapperRepository(AggregatorDbContext context)
    : Repository<CardInfoLimitWrapper>(context), ICardInfoLimitWrapperRepository;