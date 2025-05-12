using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.TokenStatusChange;

/// <summary>
/// Подробная информация об изменении статуса токена
/// </summary>
public record TokenStatusChangeDetailsDto : IHasCardIdentifier
{
    /// <summary>
    /// Идентификатор токена, связанного с PAN
    /// </summary>
    public string DpanRef { get; init; }

    /// <summary>
    /// Платежная система карты
    /// </summary>
    public string PaymentSystem { get; init; }

    /// <summary>
    /// Новый статус токена
    /// </summary>
    public string Status { get; init; }

    /// <summary>
    /// Дата создания/изменения токена в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate { get; init; }

    /// <summary>
    /// Срок действия токена (YYMM)
    /// </summary>
    public string DpanExpDate { get; init; }

    /// <summary>
    /// Идентификатор кошелька в разрезе платежной системы
    /// </summary>
    public string WalletProvider { get; init; }

    /// <summary>
    /// Имя устройства
    /// </summary>
    public string? DeviceName { get; init; }

    /// <summary>
    /// Тип устройства
    /// </summary>
    public string? DeviceType { get; init; }

    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public string? DeviceId { get; init; }

    /// <summary>
    /// Идентификатор номера карты, связанного с токеном
    /// </summary>
    public string? FpanRef { get; init; }

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