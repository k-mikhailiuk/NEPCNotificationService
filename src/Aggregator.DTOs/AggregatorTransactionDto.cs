namespace Aggregator.DTOs;

/// <summary>
/// Информация о финансовой транзакции
/// </summary>
public record AggregatorTransactionDto
{
    /// <summary>
    /// Идентификатор фин. транзакции (bo_utrnno)
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Тип транзакции в FE (используется в BO)
    /// </summary>
    public string? FeTrans { get; init; }
    
    /// <summary>
    /// Сумма транзакции в валюте счета
    /// </summary>
    public AggregatorMoneyDto? TranMoney { get; init; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты.
    /// C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? Direction {get; init;}
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto? MerchantInfo { get; init; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string? CorrespondingAccount { get; init; }
}