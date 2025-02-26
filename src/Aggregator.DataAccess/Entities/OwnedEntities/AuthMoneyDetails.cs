using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Изменение собственных средств и Exceed Limit
/// </summary>
public class AuthMoneyDetails
{
    /// <inheritdoc cref="OwnFundsMoney" />
    public OwnFundsMoney OwnFundsMoney { get; set; }
    
    /// <inheritdoc cref="ExceedLimitMoney" />
    public ExceedLimitMoney ExceedLimitMoney { get; set; }
}