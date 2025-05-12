using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Формирует конечные сообщения уведомлений на основе входных данных и справочников.
/// </summary>
public interface INotificationCompositor<T> where T : Notification
{
    /// <summary>
    /// Композирует список NotificationMessage по заданным уведомлениям и сопутствующим данным.
    /// </summary>
    /// <param name="messages">Коллекция исходных уведомлений.</param>
    /// <param name="notificationTextById">Словарь текстов уведомлений по их идентификатору.</param>
    /// <param name="notificationToCustomer">Словарь отображения идентификатора уведомления в идентификатор клиента.</param>
    /// <param name="customerSettingsMap">Словарь настроек клиента по его идентификатору.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сформированных сообщений уведомлений.</returns>
    Task<List<NotificationMessage>> ComposeAsync(IEnumerable<T> messages,
        Dictionary<long, NotificationMessageTextDirectory> notificationTextById,
        Dictionary<long, int> notificationToCustomer,
        Dictionary<int, int> customerSettingsMap,
        CancellationToken cancellationToken);
}