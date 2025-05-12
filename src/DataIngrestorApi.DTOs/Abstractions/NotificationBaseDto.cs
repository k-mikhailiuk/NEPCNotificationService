namespace DataIngrestorApi.DTOs.Abstractions;

/// <summary>
/// Базовый дто-класс уведомлений
/// </summary>
public record NotificationBaseDto
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
    /// Список расширений
    /// </summary>
    public IEnumerable<ExtensionDto>? Extensions { get; init; }
}