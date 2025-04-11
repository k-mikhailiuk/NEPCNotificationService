using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Common;
using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

/// <summary>
/// Построитель ключевых слов для уведомлений изменения статуса токена.
/// Генерирует строку с подстановкой значений для уведомления типа <see cref="TokenStatusChange"/>.
/// </summary>
public class TokenStatusChangeKeyWordBuilder : IKeyWordBuilder<TokenStatusChange>
{
    /// <summary>
    /// Словарь соответствий символов статусов для разных языков.
    /// </summary>
    private static readonly Dictionary<char, (string Ru, string En, string Kg)> StatusMap = new()
    {
        ['A'] = ("Активный","Active", "Активдүү"),
        ['I'] = ("Неактивный","Inactive", "Активдүү эмес"),
        ['S'] = ("Приостановленный","Suspended", "Токтотулган"),
        ['L'] = ("Заблокированный","Locked ", "Блоктолгон"),
    };
    
    /// <summary>
    /// Асинхронно формирует строку ключевых слов для уведомления TokenStatusChange.
    /// </summary>
    /// <param name="message">
    /// Исходное сообщение с шаблонами для подстановки (например, маркеры вида {PLACEHOLDER}).
    /// </param>
    /// <param name="entity">
    /// Объект уведомления типа <see cref="TokenStatusChange"/>, на основе которого генерируются ключевые слова.
    /// </param>
    /// <param name="language">
    /// Язык, на котором должно быть сформировано сообщение.
    /// </param>
    /// <returns>
    /// Асинхронная задача, возвращающая сформированную строку с подставленными значениями.
    /// </returns>
    public async Task<string> BuildKeyWordsAsync(string? message, TokenStatusChange entity, Language language)
    {
        var replacements = new Dictionary<string, string>
        {
            { "{PAYMENTSYSTEM}", entity.Details.PaymentSystem },
            { "{STATUS}", GetStatusName(entity.Details.Status, language) },
            { "{CHANGEDATE}", entity.Details.ChangeDate.ToString() },
            { "{DPANEXPDATE}", entity.Details.DpanExpDate },
            { "{DEVICENAME}", entity.Details.DeviceName ?? string.Empty },
            { "{DEVICEID}", entity.Details.DeviceId ?? string.Empty }
        };

        return KeyWordReplacer.ReplacePlaceholders(message, replacements);
    }
    
    /// <summary>
    /// Возвращает локализованное название статуса по символьному коду и выбранному языку.
    /// </summary>
    /// <param name="code">Символьный код статуса.</param>
    /// <param name="lang">Желаемый язык локализации.</param>
    /// <returns>Локализованное название статуса, если найдено; иначе строка "неизвестный статус" на соответствующем языке.</returns>
    private static string GetStatusName(char code, Language lang)
    {
        if (StatusMap.TryGetValue(code, out var status))
        {
            return lang switch
            {
                Language.Russian => status.Ru,
                Language.Kyrgyz => status.Kg,
                Language.English => status.En,
                _ => status.Ru
            };
        }

        return lang switch
        {
            Language.English => "Unknown status",
            Language.Russian => "Неизвестный статус",
            Language.Kyrgyz => "Белгисиз статус",
            _ => "Неизвестный статус",
        };
    }
}