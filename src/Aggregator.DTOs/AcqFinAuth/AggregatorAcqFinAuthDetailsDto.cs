namespace Aggregator.DTOs.AcqFinAuth;

/// <summary>
/// Детали онлайн эквайринговой финансовой авторизации по карте
/// </summary>
public record AggregatorAcqFinAuthDetailsDto
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; init; }
    
    /// <summary>
    /// Тип транзакции
    /// </summary>
    public int TransType { get; init; }
    
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string? ExpDate { get; init; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string? AccountId { get; init; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string CorrespondingAccount { get; init; }
    
    /// <summary>
    /// Сумма авторизации в валюте операции. Включает эквайринговую комиссию
    /// </summary>
    public AggregatorMoneyDto AuthMoney { get; init; }
    
    /// <summary>
    /// Направление движения суммы авторизации относительно карты. C - карта кредитуется, D - карта дебетуется
    /// </summary>
    public char AuthDirection { get; init; }
    
    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    public string? LocalTime { get; init; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; init; }
    
    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    public int ResponseCode { get; init; }
    
    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    public string? ApprovalCode { get; init; }
    
    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? RRN { get; init; }
    
    /// <summary>
    /// Комиссия банка-эквайера в валюте операции
    /// </summary>
    public AggregatorMoneyDto? AcqFee { get; init; }
    
    /// <summary>
    /// Направление движения эквайринговой комиссии относительно карты.
    /// Заполняется, если присутствует эквайринговая комиссия. C - карта кредитуется, D - карта дебетуется
    /// </summary>
    public char? AcqFeeDirection { get; init; }
    
    /// <summary>
    /// Сумма авторизации в валюте счета. Включает эмитентскую комиссию
    /// </summary>
    public AggregatorMoneyDto? ConvMoney { get; init; }
    
    /// <summary>
    /// Признак операции, проведенной в физическом устройстве (0 - false, 1 - true)
    /// </summary>
    public int PhysTerm { get; init; }
    
    /// <summary>
    /// Условия выполнения авторизации
    /// </summary>
    public string? AuthorizationCondition { get; init; }
    
    /// <summary>
    /// Способ ввода данных карты
    /// </summary>
    public string? PosEntryMode { get; init; }
    
    /// <summary>
    /// Направление платежа, полученное от устройства
    /// </summary>
    public string? ServiceId { get; init; }
    
    /// <summary>
    /// Сервисный код
    /// </summary>
    public string? ServiceCode { get; init; }
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public IEnumerable<AggregatorCardIdentifierDto>? CardIdentifier { get; init; }
}