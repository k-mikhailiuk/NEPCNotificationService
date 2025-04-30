using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений AcqFinAuth.
/// Генерирует строку с подстановкой значений на основе уведомления и выбранного языка.
/// </summary>
public class IssFinAuthKeyWordBuilder(ICurrencyReplacer currencyReplacer, ILimitIdReplacer limitIdReplacer)
    : IKeyWordBuilder<IssFinAuth>
{
    private static IReadOnlyDictionary<Language, string> ReversalLanguageMap { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Reversal",
            [Language.Russian] = "Отмена",
            [Language.Kyrgyz] = "Жокко чыгаруу",
        };
    
    private static IReadOnlyDictionary<Language, string> ResponseCodeMap { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Transaction declined. Please contact the bank.",
            [Language.Russian] = "Операция отклонена. Обратитесь в банк.",
            [Language.Kyrgyz] = "Операция четке кагылды. Банкка кайрылыңыз.",
        };

    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления AcqFinAuth.
    /// </summary>
    /// <param name="message">
    ///     Исходное сообщение с шаблонами для подстановки, например, содержащими маркеры вида {PLACEHOLDER}.
    /// </param>
    /// <param name="entity">
    ///     Объект уведомления типа <see cref="IssFinAuth"/> для которого необходимо сгенерировать ключевые слова.
    /// </param>
    /// <param name="language">
    ///     Язык, на котором должно быть сгенерировано сообщение.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку с подставленными значениями.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, IssFinAuth entity, Language language,
        CancellationToken cancellationToken)
    {
        var limits = new List<Limit>();
        if (entity.CardInfo?.Limits != null)
            limits.AddRange(entity.CardInfo.Limits.Select(x => x.Limit));

        limits.AddRange(entity.AccountsInfo.SelectMany(x => x.Limits.Select(l => l.Limit)));

        var limitMessages = limits.Count > 0
            ? await Task.WhenAll(limits.Select(l => GetLimitMessageAsync(l, language)))
            : [];

        var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription(language) },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : ReversalLanguageMap[language] },
            { "{PAN}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACCOUNTID}", entity.Details.AccountId ?? string.Empty },
            { "{AUTHMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AuthMoney.Amount) },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AuthMoney.Currency, cancellationToken) },
            { "{CONVMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.ConvMoney.Amount) },
            { "{CONVMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.ConvMoney.Currency, cancellationToken) },
            { "{ACCOUNTBALANCE_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AccountBalance.Amount) },
            {
                "{ACCOUNTBALANCE_CURRENCY}",
                await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AccountBalance.Currency, cancellationToken)
            },
            { "{BILLINGMONEY__AMOUNT}", NumberConverter.GetConvertedString(entity.Details.BillingMoney.Amount) },
            {
                "{BILLINGMONEY__CURRENCY}",
                await currencyReplacer.ReplaceCurrencyAsync(entity.Details.BillingMoney.Currency, cancellationToken)
            },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{COUNTRY}", entity.MerchantInfo.Country ?? string.Empty },
            { "{RESPONSECODE}", entity.Details.ResponseCode == -1 ? string.Empty : ResponseCodeMap[language] },
            {
                "{LIMIT}", limitMessages.Length > 0 ? string.Join(Environment.NewLine, limitMessages) : string.Empty
            }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }

    /// <summary>
    /// Асинхронно формирует строку описания лимита на основе его данных и выбранного языка.
    /// </summary>
    /// <param name="limit">Объект лимита.</param>
    /// <param name="language">Язык для генерации описания.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая строку с описанием лимита.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если язык не поддерживается.</exception>
    private async Task<string> GetLimitMessageAsync(Limit limit, Language language)
    {
        var limitId = await limitIdReplacer.ReplaceLimitIdAsync(limit.LimitId, language);
        var cycleType = limit.CycleType;
        var cycleLength = limit.CycleLength is not (null or 0)
            ? limit.CycleLength.ToString()
            : string.Empty;
        var endTime = limit.EndTime.ToString();

        var details = string.Empty;
        switch (limit.LimitType)
        {
            case LimitType.AmtLimit:
            {
                var trsAmount = NumberConverter.GetConvertedString(limit.TrsValue);
                var usedAmount = NumberConverter.GetConvertedString(limit.UsedValue);
                var currency = await currencyReplacer.ReplaceCurrencyAsync(limit.Currency);
                switch (language)
                {
                    case Language.Undefined:
                        return string.Empty;
                    case Language.Russian:
                        details =
                            $"Сумма лимита: {trsAmount} {currency}\nИспользовано: {usedAmount} {currency}";
                        break;
                    case Language.Kyrgyz:
                        details =
                            $"Лимит суммасы: {trsAmount} {currency}\nКолдонулду: {usedAmount} {currency}";
                        break;
                    case Language.English:
                        details =
                            $"Limit amount: {trsAmount} {currency}\nUsed: {usedAmount} {currency}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(language), language, null);
                }

                break;
            }
            case LimitType.CntLimit:
            {
                var trsValue = ((int)limit.TrsValue).ToString();
                var usedValue = ((int)limit.UsedValue).ToString();
                switch (language)
                {
                    case Language.Undefined:
                        return string.Empty;
                    case Language.Russian:
                        details = $"Количество операций: {trsValue}\nИспользовано: {usedValue}";
                        break;
                    case Language.Kyrgyz:
                        details = $"Операциялардын саны: {trsValue}\nКолдонулду: {usedValue}";
                        break;
                    case Language.English:
                        details = $"Number of operations: {trsValue}\nUsed: {usedValue}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(language), language, null);
                }

                break;
            }
            case LimitType.Undefined:
            default:
                return string.Empty;
        }
        
        var cycleInfoRu = string.IsNullOrEmpty(cycleLength)
            ? string.Empty
            : $"Длина цикла: {cycleLength} {cycleType}";
        var cycleInfoKg = string.IsNullOrEmpty(cycleLength)
            ? string.Empty
            : $"nЦикл узундугу: {cycleLength} {cycleType}";
        var cycleInfoEn = string.IsNullOrEmpty(cycleLength)
            ? string.Empty
            : $"nCycle length: {cycleLength} {cycleType}";
        
        return language switch
        {
            Language.Undefined => string.Empty,
            Language.Russian =>
                $"Лимит: {limitId}\n{cycleInfoRu}\n{details}\nДата окончания лимита: {endTime}",
            Language.Kyrgyz =>
                $"Лимит: {limitId}\n{cycleInfoKg}\n{details}\nЛимиттин аяктаган күнү: {endTime}",
            Language.English =>
                $"Limit: {limitId}\n{cycleInfoEn}\n{details}\nLimit expiration date: {endTime}",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}