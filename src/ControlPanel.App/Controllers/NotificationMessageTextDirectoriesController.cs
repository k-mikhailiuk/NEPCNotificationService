using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

/// <summary>
/// Контроллер для управления справочником текстов сообщений уведомлений.
/// </summary>
/// <remarks>
/// Предоставляет действия для получения списка текстов уведомлений и обновления текстов сообщений.
/// Доступ к методам контроллера разрешён только авторизованным пользователям.
/// </remarks>
[Authorize]
public class NotificationMessageTextDirectoriesController(
    INotificationMessageTextDirectoriesService notificationMessageTextDirectoriesService)
    : Controller
{
    /// <summary>
    /// Получает представление со списком текстов уведомлений.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>
    /// Представление, содержащее модель с данными текстов сообщений уведомлений.
    /// </returns>
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels =
            await notificationMessageTextDirectoriesService.GetNotificationsTextAsync(cancellationToken);

        return View(viewModels);
    }

    /// <summary>
    /// Обновляет тексты сообщений уведомлений.
    /// </summary>
    /// <param name="dto">
    /// Объект <see cref="UpdateNotificationMessageDirectoriesTextDto"/>, содержащий новые данные для обновления текстов сообщений.
    /// </param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>
    /// HTTP-ответ с кодом 200 (Ok) и информацией об успешном выполнении операции.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> UpdateMessageTexts([FromBody] UpdateNotificationMessageDirectoriesTextDto dto,
        CancellationToken cancellationToken = default)
    {
        await notificationMessageTextDirectoriesService.UpdateMessageTextsAsync(dto, cancellationToken);
        return Ok(new { success = true });
    }
}