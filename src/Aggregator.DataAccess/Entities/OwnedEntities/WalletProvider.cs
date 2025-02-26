using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Entities.OwnedEntities;

/// <summary>
/// Мобильный кошелек
/// </summary>
public class WalletProvider
{
    /// <summary>
    /// Идентификатор кошелька в разрезе платежной системы
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Платежная система карты
    /// </summary>
    public string PaymentSystem { get; set; }
}