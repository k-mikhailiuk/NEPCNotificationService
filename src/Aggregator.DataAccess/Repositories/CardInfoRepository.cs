using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с CardInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardInfoRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardInfo"/>.
/// </remarks>
public class CardInfoRepository(AggregatorDbContext context) : Repository<CardInfo>(context), ICardInfoRepository;