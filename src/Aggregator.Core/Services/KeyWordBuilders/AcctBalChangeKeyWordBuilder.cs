using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class AcctBalChangeKeyWordBuilder : IKeyWordBuilder<AcctBalChange>
{
    public string BuildKeyWordsAsync(string message, AcctBalChange entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : "Отмена" },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{ACCOUNTID}", entity.Details.AccountId },
            { "{PAN}", entity.CardInfo?.CardIdentifier.CardIdentifierValue ?? string.Empty },
            { "{ACCOUNT_AMOUNT}", entity.Details.AccountAmount.Amount.ToString() ?? string.Empty },
            { "{ACCOUNT_CURRENCY}", entity.Details.AccountAmount.Currency ?? string.Empty },
            { "{ACCOUNTBALANCE_AMOUNT}", entity.Details.AccountBalance.Amount.ToString() ?? string.Empty },
            { "{ACCOUNTBALANCE_CURRENCY}", entity.Details.AccountBalance.Currency ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}