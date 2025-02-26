namespace Aggregator.DataAccess.Entities.AcqFinAuth;

/// <summary>
/// Уведомление об эквайринговой финансовой авторизации по карте
/// </summary>
public class AcqFinAuth
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long AcqFinAuthId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }

    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset Time { get; set; }
    
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
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}