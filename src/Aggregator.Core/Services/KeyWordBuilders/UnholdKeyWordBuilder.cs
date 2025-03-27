using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.Unhold;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class UnholdKeyWordBuilder : IKeyWordBuilder<Unhold>
{
    public string BuildKeyWordsAsync(string message, Unhold entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription() },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : "Отмена" },
            { "{PAN}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue) },
            { "{AUTHMONEY_AMOUNT}", entity.Details.AuthMoney.Amount.ToString() ?? string.Empty },
            { "{AUTHMONEY_CURRENCY}", entity.Details.AuthMoney.Currency ?? string.Empty },
            { "{UNHOLDMONEY_AMOUNT}", entity.Details.UnholdMoney.Amount.ToString() ?? string.Empty },
            { "{UNHOLDMONEY_CURRENCY}", entity.Details.UnholdMoney.Currency ?? string.Empty },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{COUNTRY}", entity.MerchantInfo.Country ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}