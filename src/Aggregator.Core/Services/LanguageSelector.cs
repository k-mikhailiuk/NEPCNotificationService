using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services;

/// <summary>
/// Реализация интерфейса <see cref="ILanguageSelector"/> для получения идентификатора языка на основе идентификатора клиента.
/// </summary>
public class LanguageSelector : ILanguageSelector
{
    /// <summary>
    /// Асинхронно получает идентификатор языка для заданного клиента.
    /// </summary>
    /// <param name="customerId">Идентификатор клиента.</param>
    /// <param name="context">Контекст базы данных <see cref="AggregatorDbContext"/>.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая идентификатор языка как значение <see cref="long"/>, или null, если язык не найден.
    /// </returns>
    public async Task<int?> GetLanguageIdAsync(long customerId, AggregatorDbContext context,
        CancellationToken cancellationToken) =>
        await context.Database
            .SqlQuery<int?>($"SELECT LanguageId AS [Value] FROM PushNotification.Settings WHERE CustomerID = {customerId}")
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
}