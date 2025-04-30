using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Детали разовых паролей, отправляемых ACS банка-эмитента карты
/// </summary>
[Owned]
public class AcsOtpDetails
{
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