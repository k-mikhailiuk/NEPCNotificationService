using ControlPanel.Core.DTOs.Currency;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

/// <summary>
/// Сервис для управления валютами.
/// </summary>
public interface ICurrenciesService
{
    /// <summary>
    /// Возвращает список валют.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объектов Currency.</returns>
    Task<List<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Создаёт новую валюту.
    /// </summary>
    /// <param name="dto">DTO для создания валюты.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task CreateCurrency(AddCurrencyDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Редактирует данные существующей валюты.
    /// </summary>
    /// <param name="dto">DTO для редактирования валюты.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task EditCurrency(EditCurrencyDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет валюту по коду.
    /// </summary>
    /// <param name="currencyCode">Код валюты для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteCurrency(int currencyCode, CancellationToken cancellationToken);
}