using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Информация о фин. транзакции
/// </summary>
public class FinTransaction
{
    /// <summary>
    /// Идентификатор финансовой транзакции (bo_utrnno)
    /// </summary>
    [Key]
    public long FinTransactionId { get; set; }
    
    /// <summary>
    /// Тип транзакции в FE (используется в BO)
    /// </summary>
    public string? FinTrans { get; set; }
    
    /// <inheritdoc cref="TranMoney" />
    public TranMoney? TranMoney { get; set; }
    
    /// <summary>
    /// Направление движения средств относительно счета карты. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? Direction { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор MerchantInfo
    /// </summary>
    public long? MerchantInfoId { get; set; }
    
    /// <inheritdoc cref="MerchantInfo" />
    [ForeignKey(nameof(MerchantInfoId))]
    public MerchantInfo? MerchantInfo { get; set; }
    
    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string? CorrespondingAccountType { get; set; }
}