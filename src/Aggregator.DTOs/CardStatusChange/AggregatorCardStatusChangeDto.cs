namespace Aggregator.DTOs.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public class AggregatorCardStatusChangeDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; set; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string Time { get; set; }
    
    /// <summary>
    /// Подробная информация об изменении статуса карты
    /// </summary>
    public AggregatorCardStatusChangeDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public AggregatorCardInfoDto CardInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}