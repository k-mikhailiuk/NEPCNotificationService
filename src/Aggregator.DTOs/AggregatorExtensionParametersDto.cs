namespace Aggregator.DTOs;

/// <summary>
/// Параметры расширения
/// </summary>
public record AggregatorExtensionParametersDto
{
    /// <summary>
    /// Имя параметра
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Значение параметра
    /// </summary>
    public string Value { get; init; }
}