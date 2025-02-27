namespace Aggregator.DTOs;

/// <summary>
/// Информация о счете
/// </summary>
public class AggregatorAccountInfoDto
{
    /// <summary>
    /// Номер счета
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Тип счета
    /// </summary>
    public int Type { get; set; }
    
    /// <summary>
    /// Доступный баланс
    /// </summary>
    public AggregatorMoneyDto? AvailableBalance { get; set; }
    
    /// <summary>
    /// Лимит кредита
    /// </summary>
    public AggregatorMoneyDto? ExceedLimit { get; set; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public AggregatorLimitWrapperDto[]? limits { get; set; }
}