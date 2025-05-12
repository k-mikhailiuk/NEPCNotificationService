using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.Unhold;

/// <summary>
/// Подробная информация о снятии холда
/// </summary>
public class UnholdDetails
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long NotificationId { get; set; }
    
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    public long UnholdDetailsId { get; set; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public bool Reversal { get; set; }
    
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
    
    /// <inheritdoc cref="AuthMoney" />
    public AuthMoney AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты. C - счет кредитуется, D - счет дебетуется.
    /// Направление движения средств расхолдирования обратно движению средств оригинальной авторизации
    /// </summary>
    public char UnholdDirection { get; set; }
    
    /// <inheritdoc cref="UnholdMoney" />
    public UnholdMoney UnholdMoney { get; set; }
    
    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    public DateTimeOffset LocalTime { get; set; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public DateTimeOffset TransactionTime { get; set; }
    
    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    public string ApprovalCode { get; set; }
    
    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? Rrn { get; set; }
    
    /// <inheritdoc cref="IssFee" />
    public IssFee IssFee { get; set; }
    
    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета карты оригинальной авторизации.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? IssFeeDirection { get; set; }
    
    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public long? SvTrace { get; set; }
    
    /// <inheritdoc cref="WalletProvider" />
    public WalletProvider WalletProvider { get; set; }
    
    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan { get; set; }
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier CardIdentifier { get; set; }
}