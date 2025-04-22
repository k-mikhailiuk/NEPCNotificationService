using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.Unhold;

/// <summary>
/// Уведомление о снятии холда
/// </summary>
public class Unhold : Notification
{
    /// <summary>
    /// Уникальный идентификатор UnholdDetails 
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="UnholdDetails" />
    public UnholdDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    public CardInfo CardInfo { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор MerchantInfo
    /// </summary>
    public long MerchantInfoId { get; set; }
    
    /// <inheritdoc cref="MerchantInfo" />
    public MerchantInfo MerchantInfo { get; set; }
}