using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.CardStatusChange;

/// <summary>
/// Подробная информация об изменении статуса карты
/// </summary>
public record CardStatusChangeDetailsDto : IHasCardIdentifier
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; init; }
    
    /// <summary>
    /// Старое значение статуса
    /// </summary>
    public int OldStatus { get; init; }
    
    /// <summary>
    /// Новое значение статуса
    /// </summary>
    public int NewStatus { get; init; }
    
    /// <summary>
    /// Дата изменения статуса карты в ПЦ в формате (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate { get; init; }
    
    /// <summary>
    /// Сервис, изменивший статус карты
    /// </summary>
    public string? Service { get; init; }
    
    /// <summary>
    /// Пользователь сервиса, изменивший статус карты
    /// </summary>
    public string? UserName { get; init; }
    
    /// <summary>
    /// Причина изменения статуса карты
    /// </summary>
    public string? Note { get; init; }
    
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