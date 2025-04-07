using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class TokenStatusChangeKeyWordBuilder : IKeyWordBuilder<TokenStatusChange>
{
    private static readonly Dictionary<char, (string Ru, string En, string Kg)> StatusMap = new()
    {
        ['A'] = ("Активный","Active", "Активдүү"),
        ['I'] = ("Неактивный","Inactive", "Активдүү эмес"),
        ['S'] = ("Приостановленный","Suspended", "Токтотулган"),
        ['L'] = ("Заблокированный","Locked ", "Блоктолгон"),
    };
    
    public async Task<string> BuildKeyWordsAsync(string? message, TokenStatusChange entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{PAYMENTSYSTEM}", entity.Details.PaymentSystem },
            { "{STATUS}", GetStatusName(entity.Details.Status, language) },
            { "{CHANGEDATE}", entity.Details.ChangeDate.ToString() },
            { "{DPANEXPDATE}", entity.Details.DpanExpDate },
            { "{DEVICENAME}", entity.Details.DeviceName ?? string.Empty },
            { "{DEVICEID}", entity.Details.DeviceId ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
    
    private static string GetStatusName(char code, Language lang)
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