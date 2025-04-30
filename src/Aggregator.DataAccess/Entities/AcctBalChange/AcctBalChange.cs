using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.AcctBalChange;

/// <summary>
/// Уведомление об изменении лимита авторизации по факту финансовой обработки операции
/// </summary>
public class AcctBalChange : Notification
{
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
    
    /// <inheritdoc cref="AccountsInfo" />
    public List<AccountInfo> AccountsInfo { get; set; }
}