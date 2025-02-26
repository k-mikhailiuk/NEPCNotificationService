using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.Abstract;

/// <summary>
/// Информация о счете
/// </summary>
public abstract class AccountsInfo
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long Id { get; set; }
    
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