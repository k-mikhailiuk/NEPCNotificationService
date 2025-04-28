using Aggregator.DataAccess;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для получения CustomerId для уведомлений.
/// </summary>
public interface ICustomerIdSelector
{
    /// <summary>
    /// Асинхронно получает идентификатор клиента по номеру счета.
    /// </summary>
    /// <param name="accountId">Номер счета.</param>
    /// <param name="context">Контекст базы данных <see cref="AggregatorDbContext"/>.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор клиента или null, если не найден.</returns>
    Task<int?> GetCustomerIdAsync(string accountId, AggregatorDbContext context, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно получает список клиентов по номерам счета.
    /// </summary>
    /// <param name="accountIds">Номера счетов.</param>
    /// <param name="context">Контекст базы данных <see cref="AggregatorDbContext"/>.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор клиента или null, если не найден.</returns>
    Task<Dictionary<string, int?>> GetCustomerIdsAsync(
        IEnumerable<string> accountIds,
        AggregatorDbContext context,
        CancellationToken cancellationToken);

    string ParseAccountNo(string accountNumber, string bankCode);
}