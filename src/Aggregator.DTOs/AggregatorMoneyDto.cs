namespace Aggregator.DTOs;

/// <summary>
/// DTO для хранения суммы и кода валюты
/// </summary>
public record AggregatorMoneyDto
{
    /// <summary>
    /// Сумма в минимальных единицах валюты
    /// </summary>
    public decimal Amount { get; init; }
    
    /// <summary>
    /// Трехзначный числовой код валюты (ISO-4217)
    /// </summary>
    public string Currency { get; init; }
}