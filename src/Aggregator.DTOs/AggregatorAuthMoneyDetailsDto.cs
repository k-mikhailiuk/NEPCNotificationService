namespace Aggregator.DTOs;

/// <summary>
/// Изменение собственных средств и Exceed Limit
/// </summary>
public class AggregatorAuthMoneyDetailsDto
{
    /// <summary>
    /// Сумма(со знаком) изменения собственных средств
    /// </summary>
    public AggregatorMoneyDto? OwnFundsMoney { get; set; }
    
    /// <summary>
    /// Сумма(со знаком) изменения лимита кредита
    /// </summary>
    public AggregatorMoneyDto? ExceedLimitMoney { get; set; }
}