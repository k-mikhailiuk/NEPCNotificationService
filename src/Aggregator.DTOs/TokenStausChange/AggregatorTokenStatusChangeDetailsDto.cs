namespace Aggregator.DTOs.TokenStausChange;

/// <summary>
/// Подробная информация об изменении статуса токена
/// </summary>
public record AggregatorTokenStatusChangeDetailsDto
{
    /// <summary>
    /// Идентификатор токена, связанного с PAN
    /// </summary>
    public string DpanRef {get; init;}
    
    /// <summary>
    /// Платежная система карты
    /// </summary>
    public string PaymentSystem {get; init;}
    
    /// <summary>
    /// Новый статус токена
    /// </summary>
    public char Status {get; init;}
    
    /// <summary>
    /// Дата создания/изменения токена в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string ChangeDate {get; init;}
    
    /// <summary>
    /// Срок действия токена (YYMM)
    /// </summary>
    public string DpanExpDate {get; init;}
    
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
    /// Список идентификаторов карты
    /// </summary>
    public List<AggregatorCardIdentifierDto>? CardIdentifier { get; init; }
}