using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class OwiUserActionKeyWordBuilder : IKeyWordBuilder<OwiUserAction>
{
    private static readonly Dictionary<string, (string Ru, string En, string Kg)> ActionMap = new()
    {
        ["addRedPathExclusion"] = ("Добавление карты в исключение RedPath","addRedPathExclusion", "RedPath тизмесине картаны кошуу"),
        ["delRedPathExclusion"] = ("Удаление карты из исключения RedPath","delRedPathExclusion", "RedPath тизмесинен картаны өчүрүү"),
        ["resetPinCounter"] = ("Сброс счетчика ПИНа","resetPinCounter", "ПИН коду эсептегичин кайра баштоо"),
    };
    
    public async Task<string> BuildKeyWordsAsync(string? message, OwiUserAction entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo?.CardIdentifier.CardIdentifierValue) },
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACTION}", GetActionName(entity.Details.Action, language)  }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
    
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