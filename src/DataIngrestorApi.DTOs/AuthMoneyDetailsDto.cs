namespace DataIngrestorApi.DTOs;

/// <summary>
/// Изменение собственных средств и Exceed Limit
/// </summary>
public record AuthMoneyDetailsDto
{
    /// <summary>
    /// Сумма(со знаком) изменения собственных средств
    /// </summary>
    public MoneyDto? OwnFundsMoney { get; init; }
    
    /// <summary>
    /// Сумма(со знаком) изменения лимита кредита
    /// </summary>
    public MoneyDto? ExceedLimitMoney { get; init; }
}