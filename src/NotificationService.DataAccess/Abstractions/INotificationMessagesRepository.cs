using Aggregator.DataAccess.Entities;

namespace NotificationService.DataAccess.Abstractions;

/// <summary>
/// Интерфейс репозитория для работы с сущностями <see cref="NotificationMessage"/>.
/// Наследуется от базового интерфейса <see cref="IRepository{T}"/>.
/// </summary>
public interface INotificationMessagesRepository : IRepository<NotificationMessage>
{
    
}