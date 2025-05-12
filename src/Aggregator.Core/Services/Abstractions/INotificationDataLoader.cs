using Aggregator.Core.Models;
using Aggregator.DataAccess.Abstractions;
using ControlPanel.DataAccess.Abstractions;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Загружает все необходимые данные для формирования уведомлений.
/// </summary>
/// <typeparam name="T">Тип сущности уведомления.</typeparam>
public interface INotificationDataLoader<T>
{
    /// <summary>
    /// Загружает вспомогательные справочники и связи для заданных уведомлений.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="aggregatorUnitOfWork">UnitOfWork для работы с агрегаторным контекстом.</param>
    /// <param name="controlPanelUnitOfWork">UnitOfWork для работы со справочниками ControlPanel.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Объект <see cref="NotificationDataLoad{T}"/>, 
    /// содержащий исходные сообщения и связанные справочные данные.
    /// </returns>
    Task<NotificationDataLoad<T>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork, IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken);
}