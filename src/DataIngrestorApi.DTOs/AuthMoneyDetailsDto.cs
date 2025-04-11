namespace DataIngrestorApi.DTOs;

/// <summary>
/// Изменение собственных средств и Exceed Limit
/// </summary>
public class AuthMoneyDetailsDto
{
    /// <summary>
    /// Сумма(со знаком) изменения собственных средств
    /// </summary>
    public MoneyDto? OwnFundsMoney { get; set; }
    
    /// <summary>
    /// Сумма(со знаком) изменения лимита кредита
    /// </summary>
    public MoneyDto? ExceedLimitMoney { get; set; }
}