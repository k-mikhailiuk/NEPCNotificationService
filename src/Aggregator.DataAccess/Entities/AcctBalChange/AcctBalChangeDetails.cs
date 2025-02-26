using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.AcctBalChange;

/// <summary>
/// Детали операции изменения лимита авторизации по факту фин. обработки
/// </summary>
public class AcctBalChangeDetails
{
    /// <summary>
    /// Внутренний идентификатор операции изменения ЛА по факту фин. обработки
    /// </summary>
    [Key]
    public long AcctBalChangeDetailsId { get; set; }
    
    /// <summary>
    /// Признак отмены операции (0 - false, 1 - true)
    /// </summary>
    [Required]
    public bool Reversal { get; set; }
    
    /// <summary>
    /// Тип операции изменения ЛА по факту фин. обработки, (432/433)
    /// </summary>
    [Required]
    public int TransType { get; set; }
    
    /// <summary>
    /// Время регистрации операции в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset TransactionTime { get; set; }
    
    /// <inheritdoc cref="Authorization" />
    public Authorization? Auth { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор транзакции
    /// </summary>
    public long FinTransId { get; set; }
    
    /// <inheritdoc cref="FinTransaction" />
    [ForeignKey(nameof(FinTransId))]
    public FinTransaction? FinTrans { get; set; }
    
    /// <summary>
    /// Идентификатор банка эмитента
    /// </summary>
    [Required]
    public string IssInstId { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    [Required]
    public string AccountId { get; set; }
    
    /// <inheritdoc cref="AccountAmount" />
    [Required]
    public AccountAmount AccountAmount { get; set; }
    
    /// <summary>
    /// Направление движения суммы относительно счета. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    [Required]
    public char Direction { get; set; }
    
    /// <inheritdoc cref="AccountBalance" />
    [Required]
    public AccountBalance AccountBalance { get; set; }
}