using ControlPanel.Core.DTOs;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

/// <summary>
/// Сервис для управления ключевыми словами уведомлений.
/// </summary>
public interface INotificationMessageKeyWordsService
{
    /// <summary>
    /// Возвращает список ключевых слов уведомлений.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объектов <see cref="NotificationMessageKeyWord"/>.</returns>
    Task<List<NotificationMessageKeyWord>> GetKeyWordsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет описание ключевых слов уведомлений.
    /// </summary>
    /// <param name="dto">DTO, содержащее новое описание.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию обновления.</returns>
    Task UpdateDescriptionAsync(UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken);
}