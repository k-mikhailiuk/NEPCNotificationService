using ControlPanel.Core.DTOs.LimitIdDescription;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

/// <summary>
/// Контроллер для управления справочником описаний лимитов.
/// </summary>
/// <remarks>
/// Предоставляет действия для получения, создания, редактирования и удаления записей справочника описаний лимитов.
/// Доступ к контроллеру разрешён только авторизованным пользователям.
/// </remarks>
[Authorize]
public class LimitIdDescriptionDirectoriesController(ILimitIdDescriptionDirectoriesService limitIdDescriptionDirectoriesService) : Controller
{
    /// <summary>
    /// Отображает страницу со списком записей справочника описаний лимитов.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Представление, содержащее список записей справочника описаний лимитов.</returns>
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await limitIdDescriptionDirectoriesService.GetLimitIdDescriptionDirectoriesAsync(cancellationToken);
        return View(viewModels);
    }
    
    /// <summary>
    /// Создаёт новую запись справочника описаний лимитов.
    /// </summary>
    /// <param name="dto">Объект <see cref="AddLimitIdDescriptionDto"/>, содержащий данные для создания записи.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Объект результата HTTP-ответа со статусом 200 (Ok) при успешном выполнении операции.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddLimitIdDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await limitIdDescriptionDirectoriesService.CreateLimitIdDescription(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    /// <summary>
    /// Редактирует существующую запись справочника описаний лимитов.
    /// </summary>
    /// <param name="dto">Объект <see cref="EditLimitIdDescriptionDto"/>, содержащий обновлённые данные записи.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Объект результата HTTP-ответа со статусом 200 (Ok) при успешном выполнении операции.</returns>
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditLimitIdDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await limitIdDescriptionDirectoriesService.EditLimitIdDescription(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    /// <summary>
    /// Удаляет запись справочника описаний лимитов.
    /// </summary>
    /// <param name="dto">Объект <see cref="DeleteLimitIdDescriptionDto"/>, содержащий идентификатор записи для удаления.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Объект результата HTTP-ответа со статусом 200 (Ok) при успешном выполнении операции.</returns>
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteLimitIdDescriptionDto dto, CancellationToken cancellationToken = default)
    {
        await limitIdDescriptionDirectoriesService.DeleteLimitIdDescription(dto.Id, cancellationToken);
        return Ok(new { success = true });
    }
}