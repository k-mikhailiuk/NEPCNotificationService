using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class AcqFinAuthKeyWordBuilder: IKeyWordBuilder<AcqFinAuth>
{
    public string BuildKeyWordsAsync(string message, AcqFinAuth entity)
    {
       var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription() },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : "Отмена" },
            { "{PAN}", entity.Details.CardIdentifier.CardIdentifierValue ?? string.Empty },
            { "{AUTHMONEY_AMOUNT}", entity.Details.AuthMoney.Amount.ToString() ?? string.Empty },
            { "{AUTHMONEY_CURRENCY}", entity.Details.AuthMoney.Currency ?? string.Empty },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{RESPONSECODE}", entity.Details.ResponseCode == -1 ? string.Empty : "Операция отклонена. Обратитесь в банк." }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}