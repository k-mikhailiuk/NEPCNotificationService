using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.IssFinAuth;

/// <summary>
/// Детали финансовой авторизации по карте банка-эмитента
/// </summary>
public class IssFinAuthDetails
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    [Key]
    public long IssFinAuthDetailsId { get; set; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    [Required]
    public bool Reversal { get; set; }
    
    /// <summary>
    /// Тип транзакции
    /// </summary>
    [Required]
    public int TransType { get; set; }
    
    /// <summary>
    /// Идентификатор банка эмитента
    /// </summary>
    [Required]
    public string IssInstId { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    [Required]
    public string CorrespondingAccount { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string? AccountId { get; set; }
    
    /// <inheritdoc cref="AuthMoney" />
    [Required]
    public AuthMoney AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения суммы авторизации относительно счета. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    [Required]
    public char AuthDirection { get; set; }
    
    /// <inheritdoc cref="ConvMoney" />
    public ConvMoney? ConvMoney { get; set; }
    
    /// <summary>
    /// Состояние счета после операции
    /// </summary>
    public AccountBalance? AccountBalance { get; set; }
    
    /// <summary>
    /// Сумма авторизации в валюте банка-эмитента. Включает эквайринговую комиссию. Не включает эмитентскую комиссию.
    /// </summary>
    public BillingMoney? BillingMoney { get; set; }

    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset LocalTime { get; set; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset TransactionTime { get; set; }
    
    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    [Required]
    public int ResponseCode { get; set; }
    
    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    public string? ApprovalCode { get; set; }
    
    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? Rrn { get; set; }
    
    /// <inheritdoc cref="AcqFee" />
    public AcqFee? AcqFee { get; set; }
    
    /// <summary>
    /// Направление движения эквайринговой комиссии относительно счета.
    /// Заполняется, если присутствует эквайринговая комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? AcqFeeDirection { get; set; }
    
    /// <summary>
    /// Комиссия банка-эмитента в валюте счета
    /// </summary>
    public IssFee? IssFee { get; set; }
    
    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? IssFeeDirection { get; set; }
    
    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public long? SvTrace {get; set; }
    
    /// <summary>
    /// Условия выполнения авторизации
    /// </summary>
    public string? AuthorizationCondition { get; set; }
    
    /// <summary>
    /// Мобильный кошелек
    /// </summary>
    public WalletProvider? WalletProvider { get; set; }
    
    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan  { get; set; }
    
    /// <summary>
    /// Список лимитов, проверенных при авторизации. Не заполняется для отмен
    /// </summary>
    public List<CheckedLimit>? CheckedLimits { get; set; }
    
    /// <inheritdoc cref="AuthMoneyDetails" />
    public AuthMoneyDetails? AuthMoneyDetails { get; set; }
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier? CardIdentifier { get; set; } 
}