namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация о карте
/// </summary>
public class CardInfoDto
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
    public LimitWrapperDto[]? Limits { get; set; }
    
    /// <summary>
    /// Один из идентификаторов карты
    /// </summary>
    public CardIdentifierDto? CardIdentifier { get; set; }
}