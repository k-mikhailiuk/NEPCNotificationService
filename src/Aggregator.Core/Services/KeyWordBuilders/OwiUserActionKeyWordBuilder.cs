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
    /// Словарь соответствий действий для разных языков.
    /// </summary>
    private static readonly Dictionary<string, (string Ru, string En, string Kg)> ActionMap = new()
    {
        ["addRedPathExclusion"] = ("Добавление карты в исключение RedPath", "addRedPathExclusion",
            "RedPath тизмесине картаны кошуу"),
        ["delRedPathExclusion"] = ("Удаление карты из исключения RedPath", "delRedPathExclusion",
            "RedPath тизмесинен картаны өчүрүү"),
        ["resetPinCounter"] = ("Сброс счетчика ПИНа", "resetPinCounter", "ПИН коду эсептегичин кайра баштоо"),
    };

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
    /// <returns>
    /// Асинхронная задача, возвращающая строку с подставленными значениями.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, OwiUserAction entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo?.CardIdentifier.CardIdentifierValue) },
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACTION}", GetActionName(entity.Details.Action, language) }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }

    /// <summary>
    /// Возвращает локализованное название действия по ключу и выбранному языку.
    /// </summary>
    /// <param name="action">Ключ действия (строковый идентификатор).</param>
    /// <param name="language">Желаемый язык локализации.</param>
    /// <returns>Локализованное название действия, если оно найдено; иначе возвращается исходный ключ.</returns>
    private static string GetActionName(string action, Language language)
    {
        if (ActionMap.TryGetValue(action, out var status))
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