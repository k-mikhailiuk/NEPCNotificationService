using Aggregator.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с NotificationExtension.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="NotificationExtension"/>.
/// </remarks>
public interface INotificationExtensionRepository : IRepository<NotificationExtension>
{
    
}