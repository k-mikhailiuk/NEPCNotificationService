using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class CardStatusChangeKeyWordBuilder : IKeyWordBuilder<CardStatusChange>
{
    private static readonly Dictionary<int, string> StatusMap = new()
    {
        [0] = "Активный",
        [13] = "Закрытый",
        [15] = "Закрытый",
        [4] = "Неактивированный",
        [12] = "Неактивированный",
        [17] = "Неактивированный",
        [1] = "Заблокированный",
        [2] = "Заблокированный",
        [3] = "Заблокированный",
        [5] = "Заблокированный",
        [6] = "Заблокированный",
        [7] = "Заблокированный",
        [8] = "Заблокированный",
        [9] = "Заблокированный",
        [10] = "Заблокированный",
        [11] = "Заблокированный",
        [14] = "Заблокированный",
        [24] = "Заблокированный",
        [25] = "Заблокированный",
    };

    
    public string BuildKeyWordsAsync(string message, CardStatusChange entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{ACCTLDPANTAIL}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{CHANGEDATE}", entity.Details.ChangeDate.ToString() },
            { "{EXPDATE}", entity.Details.ExpDate },
            { "{NEWSTATUS}", GetStatusName(entity.Details.NewStatus) },
            { "{SERVICE}", entity.Details.Service ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }

    private static string GetStatusName(int code)
    {
        return StatusMap.TryGetValue(code, out var name)
            ? name
            : "Неизвестный статус";
    }
}