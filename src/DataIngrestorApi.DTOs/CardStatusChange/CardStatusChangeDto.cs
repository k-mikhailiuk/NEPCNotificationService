namespace DataIngrestorApi.DTOs.CardStatusChange;

/// <summary>
/// Уведомление об изменении статуса карты
/// </summary>
public record CardStatusChangeDto
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
    /// Подробная информация об изменении статуса карты
    /// </summary>
    public CardStatusChangeDetailsDto Details { get; init; }
    
    /// <summary>
    /// Информация о карте
    /// </summary>
    public CardInfoDto CardInfo { get; init; }
    
    /// <summary>
    /// Список расширений
    /// </summary>
    public List<ExtensionDto>? Extensions { get; init; }
}