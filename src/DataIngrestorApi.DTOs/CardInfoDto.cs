using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация о карте
/// </summary>
public record CardInfoDto : IHasCardIdentifier
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; init; }
    
    /// <summary>
    /// Ссылка на номер карты
    /// </summary>
    public string RefPan { get; init; }
    
    /// <summary>
    /// Идентификатор контракта
    /// </summary>
    public string ContractId { get; init; }
    
    /// <summary>
    /// Номер телефона владельца карты
    /// </summary>
    public string? MobilePhone { get; init; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public LimitWrapperDto[]? Limits { get; init; }
    
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; } = new();

    private List<CardIdentifierDto>? _cardIdentifier;
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<CardIdentifierDto>? CardIdentifier
    {
        get
        {
            if (_cardIdentifier is not null) return _cardIdentifier;
            
            _cardIdentifier = CardIdentifierJsonParser.Transform(ExtensionData);

            ExtensionData?.Clear();

            return _cardIdentifier;
        }
    }
}