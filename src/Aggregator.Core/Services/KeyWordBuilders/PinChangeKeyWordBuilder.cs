using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.PinChange;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class PinChangeKeyWordBuilder : IKeyWordBuilder<PinChange>
{
    public async Task<string> BuildKeyWordsAsync(string? message, PinChange entity, Language language)
    {
        var successStatusLanguageMap = new Dictionary<Language, string>
        {
            [Language.English] = "Successfully",
            [Language.Russian] = "Успешно",
            [Language.Kyrgyz] = "Ийгиликтүү",
        };
        
        var failureStatusLanguageMap = new Dictionary<Language, string>
        {
            [Language.English] = "Unsuccessfully",
            [Language.Russian] = "Неуспешно",
            [Language.Kyrgyz] = "Ийгиликсиз",
        };
        
        var replacements = new Dictionary<string, string>
        {
            { "{ACCTIDPANTAIL}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{EXPDATE}", entity.Details.ExpDate },
            { "{STATUS}", entity.Details.Status == "OK" ? successStatusLanguageMap[language] : failureStatusLanguageMap[language] },
            { "{SERVICE}", entity.Details.Service }
        };
        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}