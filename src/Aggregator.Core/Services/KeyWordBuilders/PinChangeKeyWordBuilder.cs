using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.PinChange;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений об изменении PIN-кода.
/// </summary>
public class PinChangeKeyWordBuilder : IKeyWordBuilder<PinChange>
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления о изменении PIN-кода.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение с шаблонами для подстановки.
    /// </param>
    /// <param name="entity">
    /// Объект уведомления <see cref="PinChange"/>, на основе которого генерируются ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должны быть сформированы ключевые слова.
    /// </param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку с подставленными значениями.
    /// </returns>
    public Task<string> BuildKeyWordsAsync(string? message, PinChange entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{ACCTIDPANTAIL}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{EXPDATE}", entity.Details.ExpDate },
            {
                "{STATUS}",
                entity.Details.Status == "OK" ? LanguageMaps.SuccessStatus[language] : LanguageMaps.FailureStatus[language]
            },
            { "{SERVICE}", entity.Details.Service }
        };
        return Task.FromResult(KeyWordReplacer.ReplacePlaceholders(message, replacements));
    }
}