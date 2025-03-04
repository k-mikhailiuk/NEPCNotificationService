using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.TokenStatusChange;

/// <summary>
/// Подробная информация об изменении статуса токена
/// </summary>
public class TokenStatusChangeDetailsDto : IHasCardIdentifier
{
    /// <summary>
    /// Идентификатор токена, связанного с PAN
    /// </summary>
    public string DpanRef { get; set; }

    /// <summary>
    /// Платежная система карты
    /// </summary>
    public string PaymentSystem { get; set; }

    /// <summary>
    /// Новый статус токена
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Дата создания/изменения токена в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate { get; set; }

    /// <summary>
    /// Срок действия токена (YYMM)
    /// </summary>
    public string DpanExpDate { get; set; }

    /// <summary>
    /// Идентификатор кошелька в разрезе платежной системы
    /// </summary>
    public string WalletProvider { get; set; }

    /// <summary>
    /// Имя устройства
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// Тип устройства
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// Идентификатор номера карты, связанного с токеном
    /// </summary>
    public string? FpanRef { get; set; }

    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> ExtensionData { get; set; } = new();

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

            ExtensionData.Clear();

            return _cardIdentifier;
        }
    }
}