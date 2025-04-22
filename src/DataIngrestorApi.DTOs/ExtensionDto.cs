namespace DataIngrestorApi.DTOs;

/// <summary>
/// Список расширений
/// </summary>
public record ExtensionDto
{
    /// <summary>
    /// Идентификатор расширения
    /// </summary>
    public string? Id { get; init; }
    
    /// <summary>
    /// Признак критичности расширения (0 - false, 1 - true)
    /// </summary>
    public int Critical { get; init; }
    
    /// <summary>
    /// Параметры расширения
    /// </summary>
    public ExtensionParametersDto[]? Parameters { get; init; }
}