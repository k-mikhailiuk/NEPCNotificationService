namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Определяет методы для асинхронной замены строки валюты.
/// </summary>
public interface ICurrencyReplacer
{
    /// <summary>
    /// Выполняет замену заданной валюты на другую строку в асинхронном режиме.
    /// </summary>
    /// <param name="currency">Исходная строка валюты для замены.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Асинхронная задача, возвращающая изменённую валюту, либо null.</returns>
    public Task<string?> ReplaceCurrencyAsync(string currency, CancellationToken cancellationToken = default);
}