using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DataAccess.Entities.Enum;
using Common;

namespace Aggregator.Core.Services.KeyWordBuilders;

public class AcsOtpKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<AcsOtp>
{
    public async Task<string> BuildKeyWordsAsync(string? message, AcsOtp entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{OTP}", entity.Details.OtpInfo.Otp },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo.CardIdentifier.CardIdentifierValue) },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{AUTHMONEY_AMOUNT}", entity.Details.AuthMoney.Amount.ToString() ?? string.Empty },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrency(entity.Details.AuthMoney.Currency) },
            { "{NAME}", entity.MerchantInfo.Name },
            { "{URL}", entity.MerchantInfo.Url },
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}