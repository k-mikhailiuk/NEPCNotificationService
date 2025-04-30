using Aggregator.DTOs.Enums;

namespace Aggregator.DTOs;

/// <summary>
/// Модель для хранения идентификатора карты
/// </summary>
public record AggregatorCardIdentifierDto
{
    /// <summary>
    /// Тип идентификатора карты
    /// </summary>
    public AggregatorCardIdentifierType Type { get; init; }
    
    /// <summary>
    /// Значение идентификатора карты
    /// </summary>
    public string? Value { get; init; }
}