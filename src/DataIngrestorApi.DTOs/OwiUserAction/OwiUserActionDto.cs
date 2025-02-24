namespace DataIngrestorApi.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public class OwiUserActionDto
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
    public OwiUserActionDetailsDto Details { get; set; }
    
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public CardInfoDto? CardInfo { get; set; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; set; }
}