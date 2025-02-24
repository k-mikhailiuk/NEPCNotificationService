using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Enums;

namespace DataIngrestorApi.DTOs.Extensions;

/// <summary>
/// Парсер для получения полей идентификатора карты
/// </summary>
public static class CardIdentifierJsonParser
{
    /// <summary>
    /// Преобразует словарь дополнительных JSON-полей в список <see cref="CardIdentifierDto"/>,
    /// фильтруя только те свойства, чьи названия соответствуют значению перечисления <see cref="CardIdentifierType"/>.
    /// </summary>
    /// <param name="dict">
    /// Словарь дополнительных полей (ключ – название поля, значение – соответствующий <see cref="JsonElement"/>).
    /// Обычно заполняется <see cref="JsonExtensionDataAttribute"/> при десериализации.
    /// </param>
    public static List<CardIdentifierDto>? Transform(Dictionary<string, JsonElement> dict)
    {
        var list = new List<CardIdentifierDto>();
        foreach (var kvp in dict)
        {
            if (Enum.TryParse<CardIdentifierType>(kvp.Key, ignoreCase: true, out var cardType))
            {
                var val = kvp.Value.GetString();
                if (!string.IsNullOrEmpty(val))
                    list.Add(new CardIdentifierDto { Type = cardType, Value = val });
            }
        }
        return list.Any() ? list : null;
    }
}