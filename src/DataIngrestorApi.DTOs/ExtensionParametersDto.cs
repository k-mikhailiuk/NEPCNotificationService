namespace DataIngrestorApi.DTOs;

/// <summary>
/// Параметр расширения
/// </summary>
public record ExtensionParametersDto
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