using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.Abstract;

/// <summary>
/// Информация о счете
/// </summary>
public class AccountInfo
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
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
    public string AccountsInfoId { get; set; }
    
    /// <summary>
    /// Тип счета
    /// </summary>
    public int Type { get; set; }
    
    /// <summary>
    /// Доступный баланс
    /// </summary>
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