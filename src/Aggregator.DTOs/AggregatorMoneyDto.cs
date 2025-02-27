namespace Aggregator.DTOs;

/// <summary>
/// DTO для хранения суммы и кода валюты
/// </summary>
public class AggregatorMoneyDto
{
    /// <summary>
    /// Сумма в минимальных единицах валюты
    /// </summary>
    public long Amount { get; set; }
    
    /// <summary>
    /// Трехзначный числовой код валюты (ISO-4217)
    /// </summary>
    public string Currency { get; set; }
}