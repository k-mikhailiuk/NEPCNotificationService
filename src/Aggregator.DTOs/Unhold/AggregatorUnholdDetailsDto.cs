namespace Aggregator.DTOs.Unhold;

/// <summary>
/// Подробная информация о снятии холда
/// </summary>
public class AggregatorUnholdDetailsDto
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; set; }
    
    /// <summary>
    /// Тип транзакции
    /// </summary>
    public int TransType { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string CorrespondingAccount { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string AccountId { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте операции. Включает эквайринговую комиссию
    /// </summary>
    public AggregatorMoneyDto AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты. C - счет кредитуется, D - счет дебетуется.
    /// Направление движения средств расхолдирования обратно движению средств оригинальной авторизации
    /// </summary>
    public string UnholdDirection { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте счета. Не включает эмитентскую комиссию
    /// </summary>
    public AggregatorMoneyDto UnholdMoney { get; set; }
    
    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    public string? LocalTime { get; set; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; set; }
    
    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    public string ApprovalCode { get; set; }
    
    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? RRN { get; set; }
    
    /// <summary>
    /// Комиссия банка-эмитента в валюте счета
    /// </summary>
    public AggregatorMoneyDto? IssFee { get; set; }
    
    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета карты оригинальной авторизации.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public string? IssFeeDirection { get; set; }
    
    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public long? SvTrace { get; set; }
    
    /// <summary>
    /// Мобильный кошелек
    /// </summary>
    public AggregatorWalletProviderDto? WalletProvider { get; set; }
    
    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan  { get; set; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<AggregatorCardIdentifierDto>? CardIdentifier { get; set; }
}