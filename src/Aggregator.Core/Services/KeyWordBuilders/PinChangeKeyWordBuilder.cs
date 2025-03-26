using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.PinChange;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class PinChangeKeyWordBuilder : IKeyWordBuilder<PinChange>
{
    public string BuildKeyWordsAsync(string message, PinChange entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{ACCTLDPANTAIL}", entity.Details.CardIdentifier.CardIdentifierValue ?? string.Empty },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{EXPDATE}", entity.Details.ExpDate },
            { "{STATUS}", entity.Details.Status == "OK" ? "Успешно" : "Неуспешно" },
            { "{SERVICE}", entity.Details.Service }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}