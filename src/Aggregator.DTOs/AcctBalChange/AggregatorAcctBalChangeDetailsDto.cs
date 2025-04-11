namespace Aggregator.DTOs.AcctBalChange;

/// <summary>
/// Детали операции изменения лимита авторизации по факту фин. обработки
/// </summary>
public class AggregatorAcctBalChangeDetailsDto
{
    /// <summary>
    /// Внутренний идентификатор операции изменения ЛА по факту фин. обработки
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Признак отмены операции (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; set; }
    
    /// <summary>
    /// Тип операции изменения ЛА по факту финансовой обработки, (432/433)
    /// </summary>
    public int TransType { get; set; }
    
    /// <summary>
    /// Время регистрации операции в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; set; }
    
    /// <summary>
    /// Информация об авторизации
    /// </summary>
    public AggregatorAuthorizationDto? Auth { get; set; }
    
    /// <summary>
    /// Информация о финансовой транзакции
    /// </summary>
    public AggregatorTransactionDto? FinTrans { get; set; }
    
    /// <summary>
    /// Идентификатор банка эмитента
    /// </summary>
    public string IssInstId { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string AccountId { get; set; }
    
    /// <summary>
    /// Сумма изменения в валюте счёта, включая комиссии
    /// </summary>
    public AggregatorMoneyDto AccountAmount { get; set; }
    
    /// <summary>
    /// Направление движения суммы относительно счета.
    /// C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char Direction { get; set; }
    
    /// <summary>
    /// Состояние счета после операции
    /// </summary>
    public AggregatorMoneyDto AccountBalance { get; set; }
}