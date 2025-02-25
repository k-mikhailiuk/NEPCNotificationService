using System.Text.Json;

namespace DataIngrestorApi.DTOs.Abstractions;

/// <summary>
/// Идентификатор карты
/// </summary>
public interface IHasCardIdentifier
{
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}