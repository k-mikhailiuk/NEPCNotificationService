using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.CardStatusChange;

/// <summary>
/// Подробная информация об изменении статуса карты
/// </summary>
public class CardStatusChangeDetailsDto : IHasCardIdentifier, IValidatableObject
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Старое значение статуса
    /// </summary>
    public int OldStatus { get; set; }
    
    /// <summary>
    /// Новое значение статуса
    /// </summary>
    public int NewStatus { get; set; }
    
    /// <summary>
    /// Дата изменения статуса карты в ПЦ в формате (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate { get; set; }
    
    /// <summary>
    /// Сервис, изменивший статус карты
    /// </summary>
    public string? Service { get; set; }
    
    /// <summary>
    /// Пользователь сервиса, изменивший статус карты
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Причина изменения статуса карты
    /// </summary>
    public string? Note { get; set; }
    
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> ExtensionData { get; set; } = new();
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    [JsonIgnore]
    public List<CardIdentifierDto>? CardIdentifier => CardIdentifierJsonParser.Transform(ExtensionData);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        CardIdentifierValidationHelper.ValidateAndCleanExtensionData(ExtensionData);
        yield break;
    }
}