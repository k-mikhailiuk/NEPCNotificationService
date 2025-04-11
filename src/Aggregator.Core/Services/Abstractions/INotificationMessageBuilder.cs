using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для построения сообщений уведомлений.
/// </summary>
/// <typeparam name="T">
/// Тип уведомления, для которого создаются сообщения. Должен реализовывать интерфейс <see cref="INotification"/>.
/// </typeparam>
public interface INotificationMessageBuilder<out T> where T : class, INotification
{
    /// <summary>
    /// Асинхронно формирует список сообщений уведомлений для заданных идентификаторов уведомлений.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений, для которых необходимо построить сообщения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая список построенных сообщений уведомлений.
    /// </returns>
    Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds, CancellationToken cancellationToken);
}