using Common.Enums;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для замены идентификатора лимита на соответствующее строковое представление в зависимости от языка.
/// </summary>
public interface ILimitIdReplacer
{
    /// <summary>
    /// Асинхронно выполняет замену идентификатора лимита на строковое представление с учетом указанного языка.
    /// </summary>
    /// <param name="limitId">Идентификатор лимита.</param>
    /// <param name="language">Язык, для которого необходимо выполнить замену.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку, представляющую замененный идентификатор лимита, или null, если замена невозможна.
    /// </returns>
    public Task<string?> ReplaceLimitIdAsync(long limitId, Language language, CancellationToken cancellationToken = default);
}