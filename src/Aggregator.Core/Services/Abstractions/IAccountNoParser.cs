namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для получения CustomerId для уведомлений.
/// </summary>
public interface IAccountNoParser
{
    /// <summary>
    /// Распарсить счет
    /// </summary>
    /// <param name="accountNumber">Номер счета в ПЦ</param>
    string ParseAccountNo(string accountNumber);
}