namespace DataIngrestorApi.DTOs;

/// <summary>
/// Информация о финансовой транзакции
/// </summary>
public class TransactionDto
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
    public MoneyDto? TranMoney { get; set; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты.
    /// C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? Direction {get; set;}
    
    /// <summary>
    /// Информация о мерчанте
    /// </summary>
    public MerchantInfoDto? MerchantInfo { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string? CorrespondingAccount { get; set; }
}