using Aggregator.DataAccess.Entities;

namespace NotificationService.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для сохранения истории отправленных уведомлений.
/// </summary>
public interface INotificationHistorySaver
{
    /// <summary>
    /// Сохраняет уведомление в историю отправок.
    /// </summary>
    /// <param name="message">Уведомление, которое необходимо сохранить.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию сохранения.</returns>
    Task SaveAsync(NotificationMessage message, CancellationToken cancellationToken = default);
}