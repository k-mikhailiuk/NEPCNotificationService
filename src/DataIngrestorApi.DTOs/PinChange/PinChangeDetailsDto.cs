using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.PinChange;

/// <summary>
/// Подробная информация об изменении PIN-кода
/// </summary>
public class PinChangeDetailsDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Время смены PIN-кода в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; set; }
    
    /// <summary>
    /// Статус операции изменения PIN-кода. OK - успешный, NOK - неуспешный
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    public int? ResponseCode { get; set; }
    
    /// <summary>
    /// Сервис по изменению PIN-кода.
    /// </summary>
    public string Service { get; set; }
    
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> ExtensionData { get; set; } = new();
    
    /// <summary>
    /// Один из идентификаторов карты
    /// </summary>
    [JsonIgnore]
    public List<CardIdentifierDto>? CardIdentifier => CardIdentifierJsonParser.Transform(ExtensionData);
}