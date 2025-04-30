using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с CardInfoLimitWrapper.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="CardInfoLimitWrapper"/>.
/// </remarks>
public interface ICardInfoLimitWrapperRepository : IRepository<CardInfoLimitWrapper>
{
    
}