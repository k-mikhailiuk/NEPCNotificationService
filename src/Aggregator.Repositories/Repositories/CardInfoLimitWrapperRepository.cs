using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с CardInfoLimitWrapper.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardInfoLimitWrapperRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardInfoLimitWrapper"/>.
/// </remarks>
public class CardInfoLimitWrapperRepository(AggregatorDbContext context)
    : Repository<CardInfoLimitWrapper>(context), ICardInfoLimitWrapperRepository;