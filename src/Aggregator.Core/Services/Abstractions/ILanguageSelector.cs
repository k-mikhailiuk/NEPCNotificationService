using Aggregator.DataAccess;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для выбора идентификатора языка на основе информации о клиенте.
/// </summary>
public interface ILanguageSelector
{
    /// <summary>
    /// Асинхронно получает идентификатор языка для заданного клиента.
    /// </summary>
    /// <param name="customerId">Идентификатор клиента.</param>
    /// <param name="context">Контекст базы данных AggregatorDbContext.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая идентификатор языка как значение <see cref="long"/>,
    /// или null, если язык не найден.
    /// </returns>
    Task<long?> GetLanguageIdAsync(long customerId, AggregatorDbContext context,
        CancellationToken cancellationToken);
}