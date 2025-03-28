using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class TokenStatusChangeKeyWordBuilder : IKeyWordBuilder<TokenStatusChange>
{
    private static readonly Dictionary<char, string> StatusMap = new()
    {
        ['A'] = "Активный",
        ['I'] = "Неактивный",
        ['S'] = "Приостановленный",
        ['L'] = "Заблокированный",
    };
    
    public string BuildKeyWordsAsync(string? message, TokenStatusChange entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{PAYMENTSYSTEM}", entity.Details.PaymentSystem },
            { "{STATUS}", GetStatusName(entity.Details.Status) },
            { "{CHANGEDATE}", entity.Details.ChangeDate.ToString() },
            { "{DPANEXPDATE}", entity.Details.DpanExpDate },
            { "{DEVICENAME}", entity.Details.DeviceName ?? string.Empty },
            { "{DEVICEID}", entity.Details.DeviceId ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
    
    private static string GetStatusName(char status)
    {
        return StatusMap.TryGetValue(status, out var name)
            ? name
            : "Неизвестный статус";
    }
}