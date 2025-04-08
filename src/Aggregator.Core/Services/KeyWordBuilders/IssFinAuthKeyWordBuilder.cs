using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class IssFinAuthKeyWordBuilder(ICurrencyReplacer currencyReplacer, ILimitIdReplacer limitIdReplacer)
    : IKeyWordBuilder<IssFinAuth>
{
    public async Task<string> BuildKeyWordsAsync(string? message, IssFinAuth entity, Language language)
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
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACCOUNTID}", entity.Details.AccountId ?? string.Empty },
            { "{AUTHMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AuthMoney.Amount) },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AuthMoney.Currency) },
            { "{CONVMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.ConvMoney.Amount) },
            { "{CONVMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.ConvMoney.Currency) },
            { "{ACCOUNTBALANCE_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AccountBalance.Amount) },
            {
                "{ACCOUNTBALANCE_CURRENCY}",
                await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AccountBalance.Currency)
            },
            { "{BILLINGMONEY__AMOUNT}", NumberConverter.GetConvertedString(entity.Details.BillingMoney.Amount) },
            {
                "{BILLINGMONEY__CURRENCY}",
                await currencyReplacer.ReplaceCurrencyAsync(entity.Details.BillingMoney.Currency)
            },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{COUNTRY}", entity.MerchantInfo.Country ?? string.Empty },
            { "{RESPONSECODE}", entity.Details.ResponseCode == -1 ? string.Empty : responseCodeMap[language] },
            {
                "{LIMIT}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(Environment.NewLine, await Task.WhenAll(entity.CardInfo.Limits.Select(async limit =>
                        await GetLimitMessageAsync(limit, language)
                    )))
                    : string.Empty
            }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }

    private async Task<string> GetLimitMessageAsync(CardInfoLimitWrapper limit, Language language)
    {
        var limitId = await limitIdReplacer.ReplaceLimitIdAsync(limit.LimitId, language);
        var cycleType = limit.Limit.CycleType;
        var cycleLength = limit.Limit.CycleLength is not (null or 0)
            ? limit.Limit.CycleLength.ToString()
            : string.Empty;
        var endTime = limit.Limit.EndTime.ToString();

        var details = string.Empty;
        switch (limit.LimitType)
        {
            case LimitType.AmtLimit:
            {
                var trsAmount = NumberConverter.GetConvertedString(limit.Limit.TrsValue);
                var usedAmount = NumberConverter.GetConvertedString(limit.Limit.UsedValue);
                var currency = await currencyReplacer.ReplaceCurrencyAsync(limit.Limit.Currency);
                switch (language)
                {
                    case Language.Undefined:
                        return string.Empty;
                    case Language.Russian:
                        details =
                            $"Лимит: {limitId}\nСумма лимита: {trsAmount} {currency}\nИспользовано: {usedAmount} {currency}";
                        break;
                    case Language.Kyrgyz:
                        details =
                            $"Лимит: {limitId}\nЛимит суммасы: {trsAmount} {currency}\nКолдонулду: {usedAmount} {currency}";
                        break;
                    case Language.English:
                        details =
                            $"Limit: {limitId}\nLimit amount: {trsAmount} {currency}\nUsed: {usedAmount} {currency}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(language), language, null);
                }

                break;
            }
            case LimitType.CntLimit:
            {
                var trsValue = ((int)limit.Limit.TrsValue).ToString();
                var usedValue = ((int)limit.Limit.UsedValue).ToString();
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

        return language switch
        {
            Language.Undefined => string.Empty,
            Language.Russian =>
                $"Лимит: {limitId}\nДлина цикла: {cycleType} {cycleLength}\n{details}\nДата окончания лимита: {endTime}",
            Language.Kyrgyz =>
                $"Лимит: {limitId}\nЦикл узундугу: {cycleType} {cycleLength}\n{details}\nЛимиттин аяктаган күнү: {endTime}",
            Language.English =>
                $"Limit: {limitId}\nCycle length: {cycleType} {cycleLength}\n{details}\nLimit expiration date: {endTime}",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}