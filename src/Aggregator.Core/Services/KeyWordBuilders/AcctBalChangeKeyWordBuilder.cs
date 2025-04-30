using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений об изменении баланса счета.
/// </summary>
public class AcctBalChangeKeyWordBuilder(ICurrencyReplacer currencyReplacer) : IKeyWordBuilder<AcctBalChange>
{
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для заданного уведомления об изменении баланса счета.
    /// </summary>
    /// <param name="message">
    ///     Исходное сообщение, содержащее шаблоны для подстановки.
    /// </param>
    /// <param name="entity">
    ///     Сущность уведомления <see cref="AcctBalChange"/>, на основе которой будут сгенерированы ключевые слова.
    /// </param>
    /// <param name="language">
    ///     Язык, на котором должны быть сформированы ключевые слова.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Асинхронная задача, возвращающая сформированную строку ключевых слов.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, AcctBalChange entity, Language language,
        CancellationToken cancellationToken)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{REVERSAL}", entity.Details.Reversal == false ? string.Empty : LanguageMaps.Reversal[language] },
            { "{TRANSATIONTIME}", entity.Details.TransactionTime.ToString() },
            { "{ACCOUNTID}", entity.Details.AccountId },
            { "{PAN}", PanMask.MaskPan(entity.CardInfo?.CardIdentifier.CardIdentifierValue) },
            { "{ACCOUNT_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AccountAmount.Amount) },
            { "{ACCOUNT_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AccountAmount.Currency, cancellationToken) },
            { "{ACCOUNTBALANCE_AMOUNT}", NumberConverter.GetConvertedString(entity.Details.AccountBalance.Amount) },
            { "{ACCOUNTBALANCE_CURRENCY}", await currencyReplacer.ReplaceCurrencyAsync(entity.Details.AccountBalance.Currency, cancellationToken) }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
}