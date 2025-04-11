using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.AcqFinAuth;

/// <summary>
/// Детали онлайн эквайринговой финансовой авторизации по карте
/// </summary>
public class AcqFinAuthDetails
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long AcqFinAuthDetailsId { get; set; }
    
    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public bool Reversal { get; set; }
    
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
    
    /// <inheritdoc cref="AuthMoney" />
    public AuthMoney AuthMoney { get; set; }
    
    /// <summary>
    /// Направление движения суммы авторизации относительно карты. C - карта кредитуется, D - карта дебетуется
    /// </summary>
    public char AuthDirection { get; set; }
    
    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    public DateTimeOffset LocalTime { get; set; }
    
    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public DateTimeOffset TransactionTime { get; set; }
    
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
    public string? Rrn { get; set; }
    
    /// <inheritdoc cref="AcqFee" />
    public AcqFee AcqFee { get; set; } 
    
    /// <summary>
    /// Направление движения эквайринговой комиссии относительно карты.
    /// Заполняется, если присутствует эквайринговая комиссия. C - карта кредитуется, D - карта дебетуется
    /// </summary>
    public char? AcqFeeDirection { get; set; }
    
    /// <inheritdoc cref="ConvMoney" />
    public ConvMoney ConvMoney { get; set; }
    
    /// <summary>
    /// Признак операции, проведенной в физическом устройстве (0 - false, 1 - true)
    /// </summary>
    public bool PhysTerm { get; set; }
    
    /// <summary>
    /// Условия выполнения авторизации
    /// </summary>
    public string? AuthorizationCondition {get; set;}
    
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
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier CardIdentifier { get; set; }
}