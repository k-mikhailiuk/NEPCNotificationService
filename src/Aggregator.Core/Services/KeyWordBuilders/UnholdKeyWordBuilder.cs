using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.Unhold;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class UnholdKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<Unhold>
{
    public async Task<string> BuildKeyWordsAsync(string? message, Unhold entity, Language language)
    {
        var reversalLanguageMap = new Dictionary<Language, string>
        {
            [Language.English] = "Reversal",
            [Language.Russian] = "Отмена",
            [Language.Kyrgyz] = "Жокко чыгару",
        };
        
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription() },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : reversalLanguageMap[language] },
            { "{PAN}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{AUTHMONEY_AMOUNT}", entity.Details.AuthMoney.Amount.ToString() ?? string.Empty },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrency(entity.Details.AuthMoney.Currency) },
            { "{UNHOLDMONEY_AMOUNT}", entity.Details.UnholdMoney.Amount.ToString() ?? string.Empty },
            { "{UNHOLDMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrency(entity.Details.UnholdMoney.Currency) },
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