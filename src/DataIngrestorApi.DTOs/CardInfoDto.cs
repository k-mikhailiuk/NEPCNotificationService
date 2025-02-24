using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация о карте
/// </summary>
public class CardInfoDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Ссылка на номер карты
    /// </summary>
    public string RefPan { get; set; }
    
    /// <summary>
    /// Идентификатор контракта
    /// </summary>
    public string ContractId { get; set; }
    
    /// <summary>
    /// Номер телефона владельца карты
    /// </summary>
    public string? MobilePhone { get; set; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public LimitWrapperDto[]? Limits { get; set; }
    
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> ExtensionData { get; set; } = new();
    
}