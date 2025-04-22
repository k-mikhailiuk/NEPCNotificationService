namespace Aggregator.DTOs;

/// <summary>
/// Список расширений
/// </summary>
public record AggregatorExtensionDto
{
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// Признак критичности расширения (0 - false, 1 - true)
    /// </summary>
    public int Critical { get; init; }
    
    /// <summary>
    /// Параметры расширения
    /// </summary>
    public AggregatorExtensionParametersDto[]? Parameters { get; init; }
}