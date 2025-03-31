using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class CardStatusChangeKeyWordBuilder : IKeyWordBuilder<CardStatusChange>
{
    private static readonly Dictionary<int, (string Ru, string En, string Kg)> StatusMap = new()
    {
        [0] = ("Активный", "Active", "Активдүү"),
        [13] = ("Закрытый", "Closed", "Жабык"),
        [15] = ("Закрытый", "Closed", "Жабык"),
        [4] = ("Неактивированный ", "Inactive", "Активдүү эмес"),
        [12] = ("Неактивированный ", "Inactive", "Активдүү эмес"),
        [17] = ("Неактивированный ", "Inactive", "Активдүү эмес"),
        [1] = ("Заблокированный", "Locked", "Блоктолгон"),
        [2] = ("Заблокированный", "Locked", "Блоктолгон"),
        [3] = ("Заблокированный", "Locked", "Блоктолгон"),
        [5] = ("Заблокированный", "Locked", "Блоктолгон"),
        [6] = ("Заблокированный", "Locked", "Блоктолгон"),
        [7] = ("Заблокированный", "Locked", "Блоктолгон"),
        [8] = ("Заблокированный", "Locked", "Блоктолгон"),
        [9] = ("Заблокированный", "Locked", "Блоктолгон"),
        [10] = ("Заблокированный", "Locked", "Блоктолгон"),
        [11] = ("Заблокированный", "Locked", "Блоктолгон"),
        [14] = ("Заблокированный", "Locked", "Блоктолгон"),
        [24] = ("Заблокированный", "Locked", "Блоктолгон"),
        [25] = ("Заблокированный", "Locked", "Блоктолгон"),
    };


    public async Task<string> BuildKeyWordsAsync(string? message, CardStatusChange entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{ACCTIDPANTAIL}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{CHANGEDATE}", entity.Details.ChangeDate.ToString() },
            { "{EXPDATE}", entity.Details.ExpDate },
            { "{NEWSTATUS}", GetStatusName(entity.Details.NewStatus, language) },
            { "{SERVICE}", entity.Details.Service ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }

    private static string GetStatusName(int code, Language lang)
    {
        if (StatusMap.TryGetValue(code, out var status))
        {
            return lang switch
            {
                Language.Russian => status.Ru,
                Language.Kyrgyz => status.Kg,
                Language.English => status.En,
                _ => status.Ru
            };
        }

        return lang switch
        {
            Language.English => "Unknown status",
            Language.Russian => "Неизвестный статус",
            Language.Kyrgyz => "Белгисиз статус",
            _ => "Неизвестный статус",
        };
    }
}