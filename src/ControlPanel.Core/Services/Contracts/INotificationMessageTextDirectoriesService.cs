using ControlPanel.Core.DTOs;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

/// <summary>
/// Сервис для управления текстами сообщений уведомлений.
/// </summary>
public interface INotificationMessageTextDirectoriesService
{
    /// <summary>
    /// Возвращает список текстов сообщений уведомлений.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объектов <see cref="NotificationMessageTextDirectory"/>.</returns>
    Task<List<NotificationMessageTextDirectory>> GetNotificationsTextAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет тексты сообщений уведомлений.
    /// </summary>
    /// <param name="dto">DTO с новыми текстами сообщений уведомлений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию обновления.</returns>
    Task UpdateMessageTextsAsync(UpdateNotificationMessageDirectoriesTextDto dto, CancellationToken cancellationToken);
}