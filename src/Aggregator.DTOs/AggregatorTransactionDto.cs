namespace Aggregator.DTOs;

/// <summary>
/// Информация о финансовой транзакции
/// </summary>
public class AggregatorTransactionDto
{
    /// <summary>
    /// Идентификатор фин. транзакции (bo_utrnno)
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Тип транзакции в FE (используется в BO)
    /// </summary>
    public string? FeTrans { get; set; }
    
    /// <summary>
    /// Сумма транзакции в валюте счета
    /// </summary>
    public AggregatorMoneyDto? TranMoney { get; set; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты.
    /// C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public string? Direction {get; set;}
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public AggregatorMerchantInfoDto? MerchantInfo { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string? CorrespondingAccount { get; set; }
}