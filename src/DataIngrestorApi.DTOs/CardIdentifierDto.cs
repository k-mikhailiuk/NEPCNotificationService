using DataIngrestorApi.DTOs.Enums;

namespace DataIngrestorApi.DTOs;

/// <summary>
/// Модель для хранения идентификатора карты
/// </summary>
public class CardIdentifierDto
{
    /// <summary>
    /// Тип идентификатора карты
    /// </summary>
    public CardIdentifierType Type { get; set; }
    
    /// <summary>
    /// Значение идентификатора карты
    /// </summary>
    public string Value { get; set; }
}