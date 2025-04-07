using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DataAccess.Entities.Enum;
using Common;
using Common.Enums;

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
            { "{AUTHMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AuthMoney.Amount) },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AuthMoney.Currency) },
            { "{NAME}", entity.MerchantInfo.Name },
            { "{URL}", entity.MerchantInfo.Url },
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}