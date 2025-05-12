using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.DTOs.Abstractions;
using DataIngrestorApi.DTOs.Extensions;

namespace DataIngrestorApi.DTOs.IssFinAuth;

/// <summary>
/// Детали финансовой авторизации по карте банка-эмитента
/// </summary>
public record IssFinAuthDetailsDto : IHasCardIdentifier
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
    /// Идентификатор банка эмитента
    /// </summary>
    public string IssInstId { get; init; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string CorrespondingAccount { get; init; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string? AccountId { get; init; }
    
    /// <summary>
    /// Сумма авторизации в валюте операции. Включает эквайринговую комиссию. Не включает эмитентскую комиссию
    /// </summary>
    public MoneyDto AuthMoney { get; init; }
    
    /// <summary>
    /// Направление движения суммы авторизации относительно счета. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char AuthDirection { get; init; }
    
    /// <summary>
    /// Сумма авторизации в валюте счета. Включает эквайринговую комиссию. Включает эмитентскую комиссию
    /// </summary>
    public MoneyDto? ConvMoney { get; init; }
    
    /// <summary>
    /// Состояние счета после операции
    /// </summary>
    public MoneyDto? AccountBalance { get; init; }
    
    /// <summary>
    /// Сумма авторизации в валюте банка-эмитента. Включает эквайринговую комиссию. Не включает эмитентскую комиссию
    /// </summary>
    public MoneyDto? BillingMoney { get; init; }
    
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
    public MoneyDto? AcqFee { get; init; }
    
    /// <summary>
    /// Направление движения эквайринговой комиссии относительно счета.
    /// Заполняется, если присутствует эквайринговая комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? AcqFeeDirection { get; init; }
    
    /// <summary>
    /// Комиссия банка-эмитента в валюте счета
    /// </summary>
    public MoneyDto? IssFee {get; init;}
    
    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? IssFeeDirection { get; init; }
    
    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public long? svTrace { get; init; }
    
    /// <summary>
    /// Условия выполнения авторизации
    /// </summary>
    public string? AuthorizationCondition { get; init; }
    
    /// <summary>
    /// Мобильный кошелек
    /// </summary>
    public WalletProviderDto? WalletProvider { get; init; }
    
    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan { get; init; }
    
    /// <summary>
    /// Список лимитов, проверенных при авторизации. Не заполняется для отмен
    /// </summary>
    public List<CheckedLimitDto>? CheckedLimits { get; init; }
    
    /// <summary>
    /// Изменение собственных средств и Exceed Limit
    /// </summary>
    public AuthMoneyDetailsDto? AuthMoneyDetails { get; init; }
    
    /// <summary>
    /// Для хранения неидентифицированных полей/заполнение CardIdentifier
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; } = new();

    private List<CardIdentifierDto>? _cardIdentifier;
    
    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public List<CardIdentifierDto>? CardIdentifier
    {
        get
        {
            if (_cardIdentifier is not null) return _cardIdentifier;
            
            _cardIdentifier = CardIdentifierJsonParser.Transform(ExtensionData);

            ExtensionData?.Clear();

            return _cardIdentifier;
        }
    }
}