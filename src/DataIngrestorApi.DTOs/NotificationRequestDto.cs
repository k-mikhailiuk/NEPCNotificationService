namespace DataIngrestorApi.DTOs;

/// <summary>
/// Модель для получения уведомлений
/// </summary>
public record NotificationRequestDto
{
    /// <summary>
    /// Количество уведомлений в списке
    /// </summary>
    public int BatchSize { get; init; }

    /// <summary>
    /// Список уведомлений
    /// </summary>
    public IEnumerable<NotificationWrapperDto> Batch { get; init; }
}