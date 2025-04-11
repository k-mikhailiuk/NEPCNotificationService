using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Common.Enums;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Интерфейс для построения ключевых слов для уведомлений.
/// </summary>
/// <typeparam name="T">
/// Тип уведомления, для которого будет сгенерирована строка ключевых слов. Должен реализовывать интерфейс <see cref="INotification"/>.
/// </typeparam>
public interface IKeyWordBuilder<in T> where T : class, INotification
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для указанного уведомления.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение или текст, используемый для генерации ключевых слов. Может быть null.
    /// </param>
    /// <param name="entity">
    /// Объект уведомления, для которого необходимо сгенерировать ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должны быть сформированы ключевые слова.
    /// </param>
    /// <returns>
    /// Асинхронная задача, возвращающая сформированную строку ключевых слов.
    /// </returns>
    Task<string> BuildKeyWordsAsync(string? message, T entity, Language language);
}