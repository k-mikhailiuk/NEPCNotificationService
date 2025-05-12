using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений OwiUserAction.
/// Генерирует строку с подстановкой значений для уведомлений типа <see cref="OwiUserAction"/>.
/// </summary>
public class OwiUserActionKeyWordBuilder : IKeyWordBuilder<OwiUserAction>
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления OwiUserAction.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение с шаблонами для подстановки.
    /// </param>
    /// <param name="entity">
    /// Объект уведомления типа <see cref="OwiUserAction"/>, на основе которого генерируются ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должны быть сформированы ключевые слова.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку с подставленными значениями.
    /// </returns>
    public Task<string> BuildKeyWordsAsync(string? message, OwiUserAction entity, Language language,
        CancellationToken cancellationToken)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo?.CardIdentifier.CardIdentifierValue) },
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACTION}", GetActionName(entity.Details.Action, language) }
        };

        return Task.FromResult(KeyWordReplacer.ReplacePlaceholders(message, replacements));
    }

    /// <summary>
    /// Возвращает локализованное название действия по ключу и выбранному языку.
    /// </summary>
    /// <param name="action">Ключ действия (строковый идентификатор).</param>
    /// <param name="language">Желаемый язык локализации.</param>
    /// <returns>Локализованное название действия, если оно найдено; иначе возвращается исходный ключ.</returns>
    private static string GetActionName(string action, Language language)
    {
        if (LanguageMaps.Action.TryGetValue(action, out var status))
        {
            return language switch
            {
                Language.Russian => status.Ru,
                Language.Kyrgyz => status.Kg,
                Language.English => status.En,
                _ => action
            };
        }

        return action;
    }
}