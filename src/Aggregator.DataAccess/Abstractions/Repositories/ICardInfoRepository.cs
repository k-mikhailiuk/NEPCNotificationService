using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с CardInfo.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="CardInfo"/>.
/// </remarks>
public interface ICardInfoRepository : IRepository<CardInfo>
{
    
}