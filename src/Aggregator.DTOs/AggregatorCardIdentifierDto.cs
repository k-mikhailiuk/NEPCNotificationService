using Aggregator.DTOs.Enums;

namespace Aggregator.DTOs;

/// <summary>
/// Модель для хранения идентификатора карты
/// </summary>
public class AggregatorCardIdentifierDto
{
    /// <summary>
    /// Тип идентификатора карты
    /// </summary>
    public AggregatorCardIdentifierType Type { get; set; }
    
    /// <summary>
    /// Значение идентификатора карты
    /// </summary>
    public string? Value { get; set; }
}