using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.Abstract;

/// <summary>
/// Информация о счете
/// </summary>
public class AccountsInfo
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long NotificationId { get; set; }
    
    /// <inheritdoc cref="NotificationType" />
    public NotificationType NotificationType { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    [Required]
    public string AccountsInfoId { get; set; }
    
    /// <summary>
    /// Тип счета
    /// </summary>
    [Required]
    public int Type { get; set; }
    
    /// <summary>
    /// Доступный баланс
    /// </summary>
    [Required]
    public AviableBalance AviableBalance { get; set; }
    
    /// <summary>
    /// Лимит кредита
    /// </summary>
    public ExceedLimitMoney ExceedLimit { get; set; }
    
    /// <summary>
    /// Список лимитов
    /// </summary>
    public List<AccountsInfoLimitWrapper>? Limits { get; set; }
}