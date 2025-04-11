namespace Aggregator.DTOs;

/// <summary>
/// Мобильный кошелек
/// </summary>
public class AggregatorWalletProviderDto
{
    /// <summary>
    /// Платежная система карты
    /// </summary>
    public string PaymentSystem { get; set; }
    
    /// <summary>
    /// Идентификатор кошелька в разрезе платежной системы
    /// </summary>
    public string? PaymentSystemId { get; set; }
}