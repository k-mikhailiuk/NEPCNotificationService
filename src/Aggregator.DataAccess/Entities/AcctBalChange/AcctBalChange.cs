using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.DataAccess.Entities.AcctBalChange;

/// <summary>
/// Уведомление об изменении лимита авторизации по факту финансовой обработки операции
/// </summary>
public class AcctBalChange
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long AcctBalChangeId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public DateTimeOffset Time { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор AcctBalChangeDetails
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="AcctBalChangeDetails" />
    public AcctBalChangeDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long? CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    public CardInfo? CardInfo { get; set; }
    
    /// <inheritdoc cref="AcctBalChangeAccountsInfo" />
    public List<AcctBalChangeAccountsInfo> AccountsInfo { get; set; }
    
    /// <inheritdoc cref="NotificationExtension" />
    public List<NotificationExtension>? Extensions { get; set; }
}