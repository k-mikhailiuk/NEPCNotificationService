using ControlPanel.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с NotificationMessageKeyWord.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="NotificationMessageKeyWord"/>.
/// </remarks>
public interface INotificationMessageKeyWordsRepository : IRepository<NotificationMessageKeyWord>
{
}