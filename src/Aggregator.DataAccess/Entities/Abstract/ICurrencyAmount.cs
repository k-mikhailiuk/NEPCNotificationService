namespace Aggregator.DataAccess.Entities.Abstract;

/// <summary>
/// Интерфейс для предоставления единой модели суммы и кода валюты
/// </summary>
public interface ICurrencyAmount
{
    /// <summary>
    /// Сумма в минимальных единицах валюты
    /// </summary>
    public long? Amount { get; set; }
    
    /// <summary>
    /// Трехзначный числовой код валюты (ISO-4217)
    /// </summary>
    public string? Currency { get; set; }
}