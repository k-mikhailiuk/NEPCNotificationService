namespace Aggregator.DTOs;

/// <summary>
/// Информация о карте
/// </summary>
public class AggregatorCardInfoDto
{
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Ссылка на номер карты
    /// </summary>
    public string RefPan { get; set; }
    
    /// <summary>
    /// Идентификатор контракта
    /// </summary>
    public string ContractId { get; set; }
    
    /// <summary>
    /// Номер телефона владельца карты
    /// </summary>
    public string? MobilePhone { get; set; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public AggregatorLimitWrapperDto[]? Limits { get; set; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<AggregatorCardIdentifierDto>? CardIdentifier { get; set; }
}