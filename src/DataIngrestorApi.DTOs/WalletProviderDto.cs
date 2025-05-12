namespace DataIngrestorApi.DTOs;

/// <summary>
/// Мобильный кошелек
/// </summary>
public record WalletProviderDto
{
    /// <summary>
    /// Платежная система карты
    /// </summary>
    public string PaymentSystem { get; init; }
    
    /// <summary>
    /// Идентификатор кошелька в разрезе платежной системы
    /// </summary>
    public string? PaymentSystemId { get; init; }
}