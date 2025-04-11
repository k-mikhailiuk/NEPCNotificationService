using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.AcsOtp;

/// <summary>
/// Уведомление о разовых паролях, отправляемых ACS банка-эмитента карты
/// </summary>
public class AcsOtp : INotification
{
    /// <summary>
    /// Уникальный идентификатор уведомления.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long NotificationId { get; set; }
    
    /// <summary>
    /// Получает или задаёт тип уведомления.
    /// </summary>
    public NotificationType NotificationType { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset Time { get; set; }
    
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
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<NotificationExtension>? Extensions { get; set; }
}