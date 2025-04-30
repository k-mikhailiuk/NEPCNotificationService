namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация о счете
/// </summary>
public record AccountInfoDto
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
    public MoneyDto? AvailableBalance { get; init; }
    
    /// <summary>
    /// Лимит кредита
    /// </summary>
    public MoneyDto? ExceedLimit { get; init; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public IEnumerable<LimitWrapperDto>? Limits { get; init; }
}