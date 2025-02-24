namespace DataIngrestorApi.DTOs;

/// <summary>
/// Мобильный кошелек
/// </summary>
public class WalletProviderDto
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