using Aggregator.DataAccess.Entities.CardStatusChange;

namespace Aggregator.DataAccess.Abstractions.Repositories.CardStatusChange;

/// <summary>
/// Интерфейс репозитория для работы с CardStatusChangeDetails.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="CardStatusChangeDetails"/>.
/// </remarks>
public interface ICardStatusChangeDetailsRepository : IRepository<CardStatusChangeDetails>
{
    
}