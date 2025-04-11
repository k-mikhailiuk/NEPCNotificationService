using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с CardInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICardInfoRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="CardInfo"/>.
/// </remarks>
public class CardInfoRepository(AggregatorDbContext context) : Repository<CardInfo>(context), ICardInfoRepository;