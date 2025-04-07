using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class AcctBalChangeKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<AcctBalChange>
{
    public async Task<string> BuildKeyWordsAsync(string? message, AcctBalChange entity, Language language)
    {
        var reversalLanguageMap = new Dictionary<Language, string>
        {
            [Language.English] = "Reversal",
            [Language.Russian] = "Отмена",
            [Language.Kyrgyz] = "Жокко чыгару",
        };
        
        var replacements = new Dictionary<string, string>
        {
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : reversalLanguageMap[language] },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{ACCOUNTID}", entity.Details.AccountId },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo?.CardIdentifier.CardIdentifierValue) },
            { "{ACCOUNT_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AccountAmount.Amount) },
            { "{ACCOUNT_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AccountAmount.Currency) },
            { "{ACCOUNTBALANCE_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AccountBalance.Amount) },
            { "{ACCOUNTBALANCE_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AccountBalance.Currency) }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}