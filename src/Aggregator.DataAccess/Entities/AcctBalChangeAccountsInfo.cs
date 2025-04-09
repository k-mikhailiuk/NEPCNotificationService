using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Информация о счете для уведомления AcctBalChange
/// </summary>
public class AcctBalChangeAccountsInfo : AccountsInfo
{
    /// <summary>
    /// Уникальный идентификатор AcctBalChange
    /// </summary>
    public long NotificationId { get; set; }
    
    public AcctBalChange.AcctBalChange AcctBalChange { get; set; }
}