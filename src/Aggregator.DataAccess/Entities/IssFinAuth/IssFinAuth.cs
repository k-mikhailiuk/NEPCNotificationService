using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;

namespace Aggregator.DataAccess.Entities.IssFinAuth;

/// <summary>
/// Уведомление о финансовой авторизации по номеру счета банка-эмитента
/// </summary>
public class IssFinAuth : INotification
{
    /// <inheritdoc />
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NotificationId { get; set; }
    
    /// <inheritdoc />
    public NotificationType NotificationType { get; set; }
    
    /// <inheritdoc />
    public long EventId { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// Идентификатор деталей финансовой авторизации по карте банка-эмитента
    /// </summary>
    public long DetailsId { get; set; }
    
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
    
    /// <inheritdoc cref="IssFinAuthAccountsInfo" />
    public List<IssFinAuthAccountsInfo> AccountsInfo { get; set; }
    
    /// <summary>
    /// Идентификатор мерчанта
    /// </summary>
    public long MerchantInfoId { get; set; }
    
    /// <inheritdoc cref="MerchantInfo" />
    public MerchantInfo MerchantInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}