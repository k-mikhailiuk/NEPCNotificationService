using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.AcqFinAuth;

/// <summary>
/// Уведомление об эквайринговой финансовой авторизации по карте
/// </summary>
public class AcqFinAuth : Notification
{
    /// <summary>
    /// Идентификатор деталей онлайн эквайринговой финансовой авторизации по карте
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="AcqFinAuthDetails" />
    public AcqFinAuthDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор MerchantInfo
    /// </summary>
    public long MerchantInfoId { get; set; }

    /// <inheritdoc cref="MerchantInfo" />
    public MerchantInfo MerchantInfo { get; set; }
}