using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.PinChange;

/// <summary>
/// Подробная информация об изменении PIN-кода
/// </summary>
public record PinChangeDetailsDto : IHasCardIdentifier
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; init; }

    /// <summary>
    /// Время смены PIN-кода в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; init; }

    /// <summary>
    /// Статус операции изменения PIN-кода. OK - успешный, NOK - неуспешный
    /// </summary>
    public string Status { get; init; }

    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    public int? ResponseCode { get; init; }

    /// <summary>
    /// Сервис по изменению PIN-кода.
    /// </summary>
    public string Service { get; init; }

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