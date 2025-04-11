using System.Text.Json;
using DataIngrestorApi.DTOs.Enums;

namespace DataIngrestorApi.DTOs.Extensions;

/// <summary>
/// Содержит вспомогательные методы для очистки словаря дополнительных полей (ExtensionData),
/// отсекая ключи, не соответствующие значениям перечисления <see cref="CardIdentifierType"/>.
/// </summary>
/// <remarks>
/// Предназначен для использования в случаях, когда модель (DTO) содержит свойство
/// <see cref="System.Text.Json.Serialization.JsonExtensionDataAttribute"/> и может принимать
/// неизвестные или необязательные поля на верхнем уровне JSON. Метод <see cref="ValidateAndCleanExtensionData"/>
/// проверяет каждое поле и удаляет те, которые не распознаются как валидный тип идентификатора.
/// </remarks>
public static class CardIdentifierValidationHelper
{
    /// <summary>
    /// Удаляет из словаря все ключи, которые не соответствуют значениям перечисления <see cref="CardIdentifierType"/>,
    /// либо имеют значение <see cref="CardIdentifierType.Undefined"/>.
    /// </summary>
    /// <param name="extensionData">
    /// Словарь дополнительных полей (как правило, полученный через атрибут <see cref="System.Text.Json.Serialization.JsonExtensionDataAttribute"/>).
    /// Ключ — название поля, значение — десериализованный <see cref="JsonElement"/>.
    /// </param>
    /// <remarks>
    /// Если <paramref name="extensionData"/> равен <c>null</c>, метод просто завершается, не выполняя действий.
    /// Все «лишние» или неизвестные поля в результате будут удалены, не генерируя ошибок.
    /// </remarks>
    public static void ValidateAndCleanExtensionData(Dictionary<string, JsonElement>? extensionData)
    {
        var invalidKeys = new List<string>();
        if (extensionData is null) return;

        foreach (var kvp in extensionData)
        {
            if (!Enum.TryParse<CardIdentifierType>(kvp.Key, ignoreCase: true, out var cardType)
                || cardType == CardIdentifierType.Undefined)
            {
                invalidKeys.Add(kvp.Key);
            }
        }

        foreach (var key in invalidKeys)
        {
            extensionData.Remove(key);
        }
    }
}