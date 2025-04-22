using DataIngrestorApi.DTOs.Enums;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Модель для хранения идентификатора карты
/// </summary>
public record CardIdentifierDto
{
    /// <summary>
    /// Тип идентификатора карты
    /// </summary>
    public CardIdentifierType Type { get; init; }
    
    /// <summary>
    /// Значение идентификатора карты
    /// </summary>
    public string? Value { get; init; }
}