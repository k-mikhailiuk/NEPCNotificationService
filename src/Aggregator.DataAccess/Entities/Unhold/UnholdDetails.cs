using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.Unhold;

/// <summary>
/// Подробная информация о снятии холда
/// </summary>
public class UnholdDetails
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    [Key]
    public long UnholdId { get; set; }
    
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
    /// Идентификатор института-корреспондента
    /// </summary>
    [Required]
    public string CorrespondingAccount { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    [Required]
    public string AccountId { get; set; }
    
    /// <inheritdoc cref="AuthMoney" />
    [Required]
    public AuthMoney AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты. C - счет кредитуется, D - счет дебетуется.
    /// Направление движения средств расхолдирования обратно движению средств оригинальной авторизации
    /// </summary>
    [Required]
    public char UnholdDirection { get; set; }
    
    /// <inheritdoc cref="UnholdMoney" />
    [Required]
    public UnholdMoney UnholdMoney { get; set; }
    
    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset LocalTime { get; set; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset TransactionDate { get; set; }
    
    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    [Required]
    public string ApprovalCode { get; set; }
    
    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? Rrn { get; set; }
    
    /// <inheritdoc cref="IssFee" />
    public IssFee? IssFee { get; set; }
    
    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета карты оригинальной авторизации.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public string? IssFeeDirection { get; set; }
    
    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public string? SvTrace { get; set; }
    
    /// <inheritdoc cref="WalletProvider" />
    public WalletProvider? WalletProvider { get; set; }
    
    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan { get; set; }
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier? CardIdentifier { get; set; }
}