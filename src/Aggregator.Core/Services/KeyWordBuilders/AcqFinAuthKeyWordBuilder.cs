using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class AcqFinAuthKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<AcqFinAuth>
{
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