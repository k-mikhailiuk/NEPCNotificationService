using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.PinChange;

/// <summary>
/// Подробная информация об изменении PIN-кода
/// </summary>
public class PinChangeDetails
{
    /// <summary>
    /// Уникальный идентификатор 
    /// </summary>
    [Key]
    public long PinChangeDetailsId { get; set; }
    
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    [Required]
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Время смены PIN-кода в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    [Required]
    public DateTimeOffset TransactionTime { get; set; }
    
    /// <summary>
    /// Статус операции изменения PIN-кода. OK - успешный, NOK - неуспешный
    /// </summary>
    [Required]
    public string Status { get; set; }
    
    /// <summary>
    /// Внутренний код ответа ПЦ
    /// </summary>
    public int? ResponseCode { get; set; }
    
    /// <summary>
    /// Сервис по изменению PIN-кода.
    /// </summary>
    [Required]
    public string Service { get; set; }
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier? CardIdentifier { get; set; }
}