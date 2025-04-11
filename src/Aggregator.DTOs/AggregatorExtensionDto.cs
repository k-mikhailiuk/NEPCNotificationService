namespace Aggregator.DTOs;

/// <summary>
/// Список расширений
/// </summary>
public class AggregatorExtensionDto
{
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Признак критичности расширения (0 - false, 1 - true)
    /// </summary>
    public int Critical { get; set; }
    
    /// <summary>
    /// Параметры расширения
    /// </summary>
    public AggregatorExtensionParametersDto[]? Parameters { get; set; }
}