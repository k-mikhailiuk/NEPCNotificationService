using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений AcqFinAuth.
/// </summary>
public class AcqFinAuthKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<AcqFinAuth>
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для заданного уведомления AcqFinAuth.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение, содержащее шаблоны для подстановки.
    /// </param>
    /// <param name="entity">
    /// Объект уведомления типа <see cref="AcqFinAuth"/>, на основе которого генерируются ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должны быть сгенерированы ключевые слова.
    /// </param>
    /// <returns>
    /// Асинхронная задача, возвращающая сформированную строку с подставленными значениями.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, AcqFinAuth entity, Language language)
    {
        var reversalLanguageMap = new Dictionary<Language, string>
        {
            [Language.English] = "Reversal",
            [Language.Russian] = "Отмена",
            [Language.Kyrgyz] = "Жокко чыгару",
        };
        
        var responseCodeMap = new Dictionary<Language, string>
        {
            [Language.English] = "Transaction declined. Please contact the bank.",
            [Language.Russian] = "Операция отклонена. Обратитесь в банк.",
            [Language.Kyrgyz] = "Операция четке кагылды. Банкка кайрылыңыз.",
        };
        
       var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription(language) },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : reversalLanguageMap[language] },
            { "{PAN}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{AUTHMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AuthMoney.Amount) },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AuthMoney.Currency) },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{RESPONSECODE}", entity.Details.ResponseCode == -1 ? string.Empty : responseCodeMap[language] }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}