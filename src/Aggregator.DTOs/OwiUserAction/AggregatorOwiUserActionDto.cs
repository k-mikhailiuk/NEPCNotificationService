namespace Aggregator.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public class AggregatorOwiUserActionDto
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
    /// Подробная информация о действии пользователя в OWI
    /// </summary>
    public AggregatorOwiUserActionDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public AggregatorCardInfoDto? CardInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<AggregatorExtensionDto>? Extensions { get; set; }
}