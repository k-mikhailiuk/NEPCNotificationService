using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с NotificationMessageTextDirectory.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="NotificationMessageTextDirectory"/>.
/// </remarks>
public interface INotificationMessageTextDirectoriesRepository : IRepository<NotificationMessageTextDirectory>
{
}