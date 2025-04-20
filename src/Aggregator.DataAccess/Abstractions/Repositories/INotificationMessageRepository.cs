using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с NotificationMessage.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="NotificationMessage"/>.
/// </remarks>
public interface INotificationMessageRepository : IRepository<NotificationMessage>
{
    
}