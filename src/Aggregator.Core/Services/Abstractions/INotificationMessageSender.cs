using Aggregator.DataAccess.Entities;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для сервиса отправки push-уведомлений.
/// </summary>
public interface INotificationMessageSender
{
    /// <summary>
    /// Отправляет указанное уведомление.
    /// </summary>
    /// <param name="message">Уведомление, которое необходимо отправить.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая результат отправки.
    /// Возвращает <c>true</c>, если отправка прошла успешно; иначе — <c>false</c>.
    /// </returns>
    Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default);
}