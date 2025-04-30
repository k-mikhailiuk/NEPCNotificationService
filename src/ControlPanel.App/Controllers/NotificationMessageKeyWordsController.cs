using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

/// <summary>
/// Контроллер для управления ключевыми словами уведомлений.
/// </summary>
/// <remarks>
/// Предоставляет действия для получения и обновления описаний ключевых слов уведомлений.
/// Доступ к контроллеру разрешён только авторизованным пользователям.
/// </remarks>
[Authorize]
public class NotificationMessageKeyWordsController(
    INotificationMessageKeyWordsService notificationMessageKeyWordsService)
    : Controller
{
    /// <summary>
    /// Получает представление со списком ключевых слов уведомлений.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>
    /// Представление с моделью, содержащей список ключевых слов уведомлений.
    /// </returns>
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await notificationMessageKeyWordsService.GetKeyWordsAsync(cancellationToken);
        
        return View(viewModels);
    }
    
    /// <summary>
    /// Обновляет описание ключевых слов уведомлений.
    /// </summary>
    /// <param name="dto">Объект <see cref="UpdateNotificationMessageKeyWordsDescriptionDto"/>, содержащий новые данные описания.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>
    /// HTTP-ответ с успешным статусом (200 Ok) при успешном обновлении.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> UpdateDescription([FromBody] UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await notificationMessageKeyWordsService.UpdateDescriptionAsync(dto, cancellationToken);
        return Ok(new { success = true });
    }
}