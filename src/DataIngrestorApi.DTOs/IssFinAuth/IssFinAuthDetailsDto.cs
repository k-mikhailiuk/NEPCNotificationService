using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.IssFinAuth;

/// <summary>
/// Детали финансовой авторизации по карте банка-эмитента
/// </summary>
public class IssFinAuthDetailsDto
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
    /// Идентификатор банка эмитента
    /// </summary>
    public string IssInstId { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string CorrespondingAccount { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string? AccountId { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте операции. Включает эквайринговую комиссию. Не включает эмитентскую комиссию
    /// </summary>
    public MoneyDto AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения суммы авторизации относительно счета. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public string AuthDirection { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте счета. Включает эквайринговую комиссию. Включает эмитентскую комиссию
    /// </summary>
    public MoneyDto? ConvMoney { get; set; }
    
    /// <summary>
    /// Состояние счета после операции
    /// </summary>
    public MoneyDto? AccountBalance { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте банка-эмитента. Включает эквайринговую комиссию. Не включает эмитентскую комиссию
    /// </summary>
    public MoneyDto? BillingMoney { get; set; }
    
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
    public MoneyDto? AcqFee { get; set; }
    
    /// <summary>
    /// Направление движения эквайринговой комиссии относительно счета.
    /// Заполняется, если присутствует эквайринговая комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public string? AcqFeeDirection { get; set; }
    
    /// <summary>
    /// Комиссия банка-эмитента в валюте счета
    /// </summary>
    public MoneyDto? IssFee {get; set;}
    
    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public string? IssFeeDirection { get; set; }
    
    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public long? svTrace { get; set; }
    
    /// <summary>
    /// Условия выполнения авторизации
    /// </summary>
    public string? AuthorizationCondition { get; set; }
    
    /// <summary>
    /// Мобильный кошелек
    /// </summary>
    public WalletProviderDto? WalletProvider { get; set; }
    
    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan { get; set; }
    
    /// <summary>
    /// Список лимитов, проверенных при авторизации. Не заполняется для отмен
    /// </summary>
    public List<CheckedLimitDto>? CheckedLimits { get; set; }
    
    /// <summary>
    /// Изменение собственных средств и Exceed Limit
    /// </summary>
    public AuthMoneyDetailsDto? AuthMoneyDetails { get; set; }
    
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> ExtensionData { get; set; } = new();
    
    /// <summary>
    /// Один из идентификаторов карты
    /// </summary>
    [JsonIgnore]
    public List<CardIdentifierDto>? CardIdentifier => CardIdentifierJsonParser.Transform(ExtensionData);
}