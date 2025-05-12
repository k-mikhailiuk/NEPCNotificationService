using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.Unhold;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений Unhold.
/// Генерирует строку с подстановкой значений для уведомлений типа <see cref="Unhold"/>.
/// </summary>
public class UnholdKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<Unhold>
{
    private static IReadOnlyDictionary<Language, string> ReversalLanguageMap { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Reversal",
            [Language.Russian] = "Отмена",
            [Language.Kyrgyz] = "Жокко чыгаруу",
        };

    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления Unhold.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение с шаблонами для подстановки (например, маркеры {PLACEHOLDER}).
    /// </param>
    /// <param name="entity">
    /// Объект уведомления типа <see cref="Unhold"/>, на основе которого генерируются ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должно быть сформировано сообщение.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку с подставленными значениями.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, Unhold entity, Language language,
        CancellationToken cancellationToken)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription(language) },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : ReversalLanguageMap[language] },
            { "{PAN}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{AUTHMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AuthMoney.Amount) },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AuthMoney.Currency, cancellationToken) },
            { "{UNHOLDMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.UnholdMoney.Amount) },
            { "{UNHOLDMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.UnholdMoney.Currency, cancellationToken) },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{COUNTRY}", entity.MerchantInfo.Country ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}