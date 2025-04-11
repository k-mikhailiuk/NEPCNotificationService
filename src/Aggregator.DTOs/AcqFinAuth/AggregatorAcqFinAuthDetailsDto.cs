namespace Aggregator.DTOs.AcqFinAuth;

/// <summary>
/// Детали онлайн эквайринговой финансовой авторизации по карте
/// </summary>
public class AggregatorAcqFinAuthDetailsDto
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
    /// Срок действия карты (YYMM)
    /// </summary>
    public string? ExpDate { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string? AccountId { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string CorrespondingAccount { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте операции. Включает эквайринговую комиссию
    /// </summary>
    public AggregatorMoneyDto AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения суммы авторизации относительно карты. C - карта кредитуется, D - карта дебетуется
    /// </summary>
    public char AuthDirection { get; set; }
    
    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    public string? LocalTime { get; set; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; set; }
    
    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    public int ResponseCode { get; set; }
    
    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    public string? ApprovalCode { get; set; }
    
    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? RRN { get; set; }
    
    /// <summary>
    /// Комиссия банка-эквайера в валюте операции
    /// </summary>
    public AggregatorMoneyDto? AcqFee { get; set; }
    
    /// <summary>
    /// Направление движения эквайринговой комиссии относительно карты.
    /// Заполняется, если присутствует эквайринговая комиссия. C - карта кредитуется, D - карта дебетуется
    /// </summary>
    public char? AcqFeeDirection { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте счета. Включает эмитентскую комиссию
    /// </summary>
    public AggregatorMoneyDto? ConvMoney { get; set; }
    
    /// <summary>
    /// Признак операции, проведенной в физическом устройстве (0 - false, 1 - true)
    /// </summary>
    public int PhysTerm { get; set; }
    
    /// <summary>
    /// Условия выполнения авторизации
    /// </summary>
    public string? AuthorizationCondition { get; set; }
    
    /// <summary>
    /// Способ ввода данных карты
    /// </summary>
    public string? PosEntryMode { get; set; }
    
    /// <summary>
    /// Направление платежа, полученное от устройства
    /// </summary>
    public string? ServiceId { get; set; }
    
    /// <summary>
    /// Сервисный код
    /// </summary>
    public string? ServiceCode { get; set; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<AggregatorCardIdentifierDto>? CardIdentifier { get; set; }
}