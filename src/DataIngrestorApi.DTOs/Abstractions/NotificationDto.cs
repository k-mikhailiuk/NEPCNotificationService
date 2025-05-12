namespace DataIngrestorApi.DTOs.Abstractions;

/// <summary>
/// Базовый обобщенный класс уведомлений для их деталей
/// </summary>
/// <typeparam name="TDetails"></typeparam>
public record NotificationDto<TDetails> : NotificationBaseDto
{
    /// <summary>
    /// Детали уведомления
    /// </summary>
    public TDetails Details { get; set; }
}