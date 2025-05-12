using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.AcsOtp;

/// <summary>
/// Детали разовых паролей, отправляемых ACS банка-эмитента карты
/// </summary>
public class AcsOtpDetails
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long NotificationId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор деталей уведомления одноразового кода
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <summary>
    /// Информация о разовом пароле
    /// </summary>
    public OtpInfo OtpInfo { get; set; }
    
    /// <summary>
    /// Сумма операции. Может отсутствовать для нефинансовых операций
    /// </summary>
    public AuthMoney? AuthMoney { get; set; }
    
    /// <summary>
    /// Время операции (YYYYMMDDHH24MISS) во временной зоне ПЦ. Может отсутствовать для нефинансовых операций
    /// </summary>
    public DateTimeOffset TransactionTime { get; set; }
}