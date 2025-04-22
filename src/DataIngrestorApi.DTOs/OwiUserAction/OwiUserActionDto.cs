namespace DataIngrestorApi.DTOs.OwiUserAction;

/// <summary>
/// Уведомление о действии пользователя в OWI
/// </summary>
public record OwiUserActionDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор события
    /// </summary>
    public long EventId { get; init; }
    
    /// <summary>
    /// Время создания уведомления (YYYYMMDDHH24MISS) во временной зоне ПЦ
    /// </summary>
    public string Time { get; init; }
    
    /// <summary>
    /// Подробная информация о действии пользователя в OWI
    /// </summary>
    public OwiUserActionDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте на момент формирования уведомления
    /// </summary>
    public CardInfoDto? CardInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; init; }
}