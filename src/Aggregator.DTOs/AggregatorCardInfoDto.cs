namespace Aggregator.DTOs;

/// <summary>
/// Информация о карте
/// </summary>
public record AggregatorCardInfoDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; init; }
    
    /// <summary>
    /// Ссылка на номер карты
    /// </summary>
    public string RefPan { get; init; }
    
    /// <summary>
    /// Идентификатор контракта
    /// </summary>
    public string ContractId { get; init; }
    
    /// <summary>
    /// Номер телефона владельца карты
    /// </summary>
    public string? MobilePhone { get; init; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public AggregatorLimitWrapperDto[]? Limits { get; init; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<AggregatorCardIdentifierDto>? CardIdentifier { get; init; }
}