using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.OwnedEntities;

namespace Aggregator.DataAccess.Entities.CardStatusChange;

/// <summary>
/// Подробная информация об изменении статуса карты
/// </summary>
public class CardStatusChangeDetails
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public long CardStatusChangeDetailsId { get; set; }
    
    /// <summary>
    /// Срок действия карты (YYMM)
    /// </summary>
    public string ExpDate { get; set; }
    
    /// <summary>
    /// Старое значение статуса
    /// </summary>
    public int OldStatus { get; set; }
    
    /// <summary>
    /// Новое значение статуса
    /// </summary>
    public int NewStatus { get; set; }
    
    /// <summary>
    /// Дата изменения статуса карты в ПЦ в формате (YYYYMMDDHH24MISS)
    /// </summary>
    public DateTimeOffset ChangeDate { get; set; }
    
    /// <summary>
    /// Сервис, изменивший статус карты
    /// </summary>
    public string? Service { get; set; }
    
    /// <summary>
    /// Пользователь сервиса, изменивший статус карты
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Причина изменения статуса карты
    /// </summary>
    public string? Note { get; set; }
    
    /// <inheritdoc cref="CardIdentifier" />
    public CardIdentifier? CardIdentifier { get; set; }
}