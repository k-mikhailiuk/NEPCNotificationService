using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений AcsOtp.
/// </summary>
public class AcsOtpKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<AcsOtp>
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления AcsOtp.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение с шаблонами для подстановки.
    /// </param>
    /// <param name="entity">
    /// Объект уведомления типа <see cref="AcsOtp"/>, на основе которого генерируются ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должны быть сформированы ключевые слова.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Асинхронная задача, возвращающая сформированную строку с подставленными значениями.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, AcsOtp entity, Language language,
        CancellationToken cancellationToken)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{OTP}", entity.Details.OtpInfo.Otp },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo.CardIdentifier.CardIdentifierValue) },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{AUTHMONEY_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AuthMoney.Amount) },
            { "{AUTHMONEY_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AuthMoney.Currency, cancellationToken) },
            { "{NAME}", entity.MerchantInfo.Name },
            { "{URL}", entity.MerchantInfo.Url },
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}