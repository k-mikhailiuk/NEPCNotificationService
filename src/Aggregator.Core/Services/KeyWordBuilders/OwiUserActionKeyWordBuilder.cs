using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class OwiUserActionKeyWordBuilder : IKeyWordBuilder<OwiUserAction>
{
    private static readonly Dictionary<string, string> ActionMap = new()
    {
        ["addRedPathExclusion"] = "Добавление карты в исключение RedPath",
        ["delRedPathExclusion"] = "Удаление карты из исключения RedPath",
        ["resetPinCounter"] = "Сброс счетчика ПИНа",
    };
    
    public string BuildKeyWordsAsync(string? message, OwiUserAction entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo?.CardIdentifier.CardIdentifierValue) },
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACTION}", GetActionName(entity.Details.Action)  }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
    
    private static string GetActionName(string action)
    {
        return ActionMap.TryGetValue(action, out var name)
            ? name
            : action;
    }
}