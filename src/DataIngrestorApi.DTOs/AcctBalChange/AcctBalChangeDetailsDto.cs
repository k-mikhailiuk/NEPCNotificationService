namespace DataIngrestorApi.DTOs.AcctBalChange;

/// <summary>
/// Детали операции изменения лимита авторизации по факту фин. обработки
/// </summary>
public record AcctBalChangeDetailsDto
{
    /// <summary>
    /// Внутренний идентификатор операции изменения ЛА по факту фин. обработки
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Признак отмены операции (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; init; }
    
    /// <summary>
    /// Тип операции изменения ЛА по факту финансовой обработки, (432/433)
    /// </summary>
    public int TransType { get; init; }
    
    /// <summary>
    /// Время регистрации операции в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; init; }
    
    /// <summary>
    /// Информация об авторизации
    /// </summary>
    public AuthorizationDto? Auth { get; init; }
    
    /// <summary>
    /// Информация о финансовой транзакции
    /// </summary>
    public TransactionDto? FinTrans { get; init; }
    
    /// <summary>
    /// Идентификатор банка эмитента
    /// </summary>
    public string IssInstId { get; init; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string AccountId { get; init; }
    
    /// <summary>
    /// Сумма изменения в валюте счёта, включая комиссии
    /// </summary>
    public MoneyDto AccountAmount { get; init; }
    
    /// <summary>
    /// Направление движения суммы относительно счета.
    /// C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char Direction { get; init; }
    
    /// <summary>
    /// Состояние счета после операции
    /// </summary>
    public MoneyDto AccountBalance { get; init; }
}