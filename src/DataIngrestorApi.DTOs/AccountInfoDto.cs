namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация о счете
/// </summary>
public class AccountInfoDto
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
    public MoneyDto? AvailableBalance { get; set; }
    
    /// <summary>
    /// Лимит кредита
    /// </summary>
    public MoneyDto? ExceedLimit { get; set; }
    
    /// <summary>
    /// Тип - контейнер лимитов
    /// </summary>
    public LimitWrapperDto[]? Limits { get; set; }
}