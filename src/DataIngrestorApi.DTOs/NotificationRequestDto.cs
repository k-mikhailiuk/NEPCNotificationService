namespace DataIngrestorApi.DTOs;

/// <summary>
/// Модель для получения уведомлений
/// </summary>
public class NotificationRequestDto
{
    /// <summary>
    /// Количество уведомлений в списке
    /// </summary>
    public int BatchSize { get; set; }

    /// <summary>
    /// Список уведомлений
    /// </summary>
    public NotificationWrapperDto[] Batch { get; set; }
}