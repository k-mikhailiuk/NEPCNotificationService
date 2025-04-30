using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.AcsOtp;

/// <summary>
/// Уведомление о разовых паролях, отправляемых ACS банка-эмитента карты
/// </summary>
public class AcsOtp : Notification
{
    /// <summary>
    /// Детали разовых паролей, отправляемых ACS банка-эмитента карты
    /// </summary>
    public AcsOtpDetails Details { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public long CardInfoId { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfo CardInfo { get; set; }
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AcsOtpMerchantInfo MerchantInfo { get; set; }
}