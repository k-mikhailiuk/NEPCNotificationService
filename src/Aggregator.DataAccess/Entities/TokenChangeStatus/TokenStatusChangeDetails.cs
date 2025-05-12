using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.TokenChangeStatus;

/// <summary>
/// Подробная информация об изменении статуса токена
/// </summary>
public class TokenStatusChangeDetails
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long NotificationId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long TokenStatusChangeDetailsId { get; set; }
    
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
    public char Status { get; set; }
    
    /// <summary>
    /// Дата создания/изменения токена в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public DateTimeOffset ChangeDate { get; set; }
    
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
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier CardIdentifier { get; set; }
}