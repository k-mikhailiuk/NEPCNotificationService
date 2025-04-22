namespace Aggregator.DTOs;

/// <summary>
/// Информация о счете
/// </summary>
public record AggregatorAccountInfoDto
{
    /// <summary>
    /// Номер счета
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// Тип счета
    /// </summary>
    public int Type { get; init; }
    
    /// <summary>
    /// Доступный баланс
    /// </summary>
    public AggregatorMoneyDto? AvailableBalance { get; init; }
    
    /// <summary>
    /// Лимит кредита
    /// </summary>
    public AggregatorMoneyDto? ExceedLimit { get; init; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public AggregatorLimitWrapperDto[]? Limits { get; init; }
}