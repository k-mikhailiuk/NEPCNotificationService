using ControlPanel.Core.DTOs;
using ControlPanel.Core.DTOs.Currency;
using ControlPanel.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.App.Controllers;

/// <summary>
/// Контроллер для управления валютами.
/// </summary>
/// <remarks>
/// Предоставляет действия для получения, создания, редактирования и удаления валют.
/// Доступ к методам контроллера разрешён только авторизованным пользователям.
/// </remarks>
[Authorize]
public class CurrenciesController(ICurrenciesService currenciesService) : Controller
{
    /// <summary>
    /// Получает список валют.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Представление с моделью, содержащей список валют.</returns>
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var viewModels = await currenciesService.GetCurrenciesAsync(cancellationToken);
        
        return View(viewModels);
    }
    
    /// <summary>
    /// Создаёт новую валюту.
    /// </summary>
    /// <param name="dto">Объект <see cref="AddCurrencyDto"/>, содержащий данные для создания валюты.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>
    /// Возвращает статус 200 (Ok) с информацией об успешном выполнении операции.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await currenciesService.CreateCurrency(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    /// <summary>
    /// Редактирует данные существующей валюты.
    /// </summary>
    /// <param name="dto">Объект <see cref="EditCurrencyDto"/>, содержащий обновлённые данные валюты.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>
    /// Возвращает статус 200 (Ok) с информацией об успешном выполнении операции.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await currenciesService.EditCurrency(dto, cancellationToken);
        return Ok(new { success = true });
    }
    
    /// <summary>
    /// Удаляет валюту.
    /// </summary>
    /// <param name="dto">Объект <see cref="DeleteCurrencyDto"/>, содержащий код валюты для удаления.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>
    /// Возвращает статус 200 (Ok) с информацией об успешном выполнении операции.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] DeleteCurrencyDto dto, CancellationToken cancellationToken = default)
    {
        await currenciesService.DeleteCurrency(dto.CurrencyCode, cancellationToken);
        return Ok(new { success = true });
    }
}