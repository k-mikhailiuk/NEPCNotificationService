using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public class IssFinAuth : Notification
{
    /// <inheritdoc cref="IssFinAuthDetails" />
    public IssFinAuthDetails Details { get; set; }
    
    /// <summary>
    /// Идентификатор информации о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public long? CardInfoId { get; set; }
    
    /// <summary>
    /// Информация о карте и ее лимитах на момент формирования уведомления
    /// </summary>
    public CardInfo? CardInfo { get; set; }
    
    /// <inheritdoc cref="AccountsInfo" />
    public List<AccountInfo> AccountsInfo { get; set; }
    
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public long MerchantInfoId { get; set; }
    
    /// <inheritdoc cref="MerchantInfo" />
    public MerchantInfo MerchantInfo { get; set; }
}