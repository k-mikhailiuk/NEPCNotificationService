using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений изменения статуса карты.
/// </summary>
public class CardStatusChangeKeyWordBuilder : IKeyWordBuilder<CardStatusChange>
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления изменения статуса карты.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение с шаблонами для подстановки.
    /// </param>
    /// <param name="entity">
    /// Сущность <see cref="CardStatusChange"/>, на основе которой будут сгенерированы ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должны быть сформированы ключевые слова.
    /// </param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку с подставленными значениями.
    /// </returns>
    public Task<string> BuildKeyWordsAsync(string? message, CardStatusChange entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{ACCTIDPANTAIL}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{CHANGEDATE}", entity.Details.ChangeDate.ToString() },
            { "{EXPDATE}", entity.Details.ExpDate },
            { "{NEWSTATUS}", GetStatusName(entity.Details.NewStatus, language) },
            { "{SERVICE}", entity.Details.Service ?? string.Empty }
        };

        return Task.FromResult(KeyWordReplacer.ReplacePlaceholders(message, replacements));
    }

    /// <summary>
    /// Возвращает название статуса по коду и языку.
    /// </summary>
    /// <param name="code">Целочисленный код статуса.</param>
    /// <param name="lang">Язык, на котором необходимо вернуть описание статуса.</param>
    /// <returns>Локализованное название статуса, либо строка "неизвестный статус" на соответствующем языке.</returns>
    private static string GetStatusName(int code, Language lang)
    {
        if (LanguageMaps.Status.TryGetValue(code, out var status))
        {
            return lang switch
            {
                Language.Russian => status.Ru,
                Language.Kyrgyz => status.Kg,
                Language.English => status.En,
                _ => status.Ru
            };
        }

        return lang switch
        {
            Language.English => "Unknown status",
            Language.Russian => "Неизвестный статус",
            Language.Kyrgyz => "Белгисиз статус",
            _ => "Неизвестный статус",
        };
    }
}