using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public class OwiUserAction : Notification
{
    /// <summary>
    /// Уникальный идентификатор OwiUserActionDetails
    /// </summary>
    public long DetailsId { get; set; }
    
    /// <inheritdoc cref="OwiUserActionDetails" />
    public OwiUserActionDetails Details { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор CardInfo
    /// </summary>
    public long? CardInfoId { get; set; }
    
    /// <inheritdoc cref="CardInfo" />
    public CardInfo? CardInfo { get; set; }
}