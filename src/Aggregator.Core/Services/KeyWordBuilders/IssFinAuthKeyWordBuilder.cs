using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class IssFinAuthKeyWordBuilder : IKeyWordBuilder<IssFinAuth>
{
    public string BuildKeyWordsAsync(string? message, IssFinAuth entity)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{TRANSTYPE}", ((TransType)entity.Details.TransType).GetDescription() },
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : "Отмена" },
            { "{PAN}", PanMask.MaskPan(entity.Details.CardIdentifier.CardIdentifierValue)},
            { "{EXPDATE}", entity.CardInfo?.ExpDate ?? string.Empty },
            { "{ACCOUNTID}", entity.Details.AccountId ?? string.Empty },
            { "{AUTHMONEY_AMOUNT}", entity.Details.AuthMoney.Amount.ToString() ?? string.Empty },
            { "{AUTHMONEY_CURRENCY}", entity.Details.AuthMoney.Currency ?? string.Empty },
            { "{CONVMONEY_AMOUNT}", entity.Details.ConvMoney.Amount.ToString() ?? string.Empty },
            { "{CONVMONEY_CURRENCY}", entity.Details.ConvMoney.Currency ?? string.Empty },
            { "{ACCOUNTBALANCE_AMOUNT}", entity.Details.AccountBalance.Amount.ToString() ?? string.Empty },
            { "{ACCOUNTBALANCE_CURRENCY}", entity.Details.AccountBalance.Currency ?? string.Empty },
            { "{BILLINGMONEY__AMOUNT}", entity.Details.BillingMoney.Amount.ToString() ?? string.Empty },
            { "{BILLINGMONEY__CURRENCY}", entity.Details.BillingMoney.Currency ?? string.Empty },
            { "{LOCALTIME}", entity.Details.LocalTime.ToString() },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{RRN}", entity.Details.Rrn ?? string.Empty },
            { "{TERMINALID}", entity.MerchantInfo.TerminalId ?? string.Empty },
            { "{NAME}", entity.MerchantInfo.Name ?? string.Empty },
            { "{CITY}", entity.MerchantInfo.City ?? string.Empty },
            { "{COUNTRY}", entity.MerchantInfo.Country ?? string.Empty },
            {
                "{CYCLETYPE}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ", entity.CardInfo.Limits.Select(limit => limit.Limit.CycleType))
                    : string.Empty
            },
            {
                "{CYCLELENGTH}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ", entity.CardInfo.Limits.Select(limit => limit.Limit.CycleLength))
                    : string.Empty
            },
            {
                "{ENDTIME}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ", entity.CardInfo.Limits.Select(limit => limit.Limit.EndTime))
                    : string.Empty
            },
            {
                "{TRSAMOUNT}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ",
                        entity.CardInfo.Limits.Select(limit =>
                            limit.LimitType == LimitType.AmtLimit ? limit.Limit.TrsValue.ToString() : string.Empty))
                    : string.Empty
            },
            {
                "{USEDAMOUNT}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ",
                        entity.CardInfo.Limits.Select(limit =>
                            limit.LimitType == LimitType.AmtLimit ? limit.Limit.UsedValue.ToString() : string.Empty))
                    : string.Empty
            },
            {
                "{AMOUNTLIMIT_CURRENCY}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ", entity.CardInfo.Limits.Select(limit => limit.Limit.Currency))
                    : string.Empty
            },
            {
                "{TRSVALUE}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ",
                        entity.CardInfo.Limits.Select(limit =>
                            limit.LimitType == LimitType.CntLimit ? limit.Limit.TrsValue.ToString() : string.Empty))
                    : string.Empty
            },
            {
                "{USEDVALUE}",
                entity.CardInfo?.Limits != null && entity.CardInfo.Limits.Count != 0
                    ? string.Join(", ",
                        entity.CardInfo.Limits.Select(limit =>
                            limit.LimitType == LimitType.CntLimit ? limit.Limit.UsedValue.ToString() : string.Empty))
                    : string.Empty
            },
            { "{RESPONSECODE}", entity.Details.ResponseCode == -1 ? string.Empty : "Операция отклонена. Обратитесь в банк." }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}