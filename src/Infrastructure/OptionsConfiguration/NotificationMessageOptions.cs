namespace OptionsConfiguration;

/// <summary>
/// Класс конфигурации параметров сообщений уведомлений.
/// Используется для привязки значений из конфигурационной секции <c>NotificationMessage</c>.
/// </summary>
public class NotificationMessageOptions
{
    /// <summary>
    /// Имя секции конфигурации.
    /// </summary>
    public const string NotificationMessage = nameof(NotificationMessage);
    
    /// <summary>
    /// Заголовок уведомления.
    /// </summary>
    public string Title { get; set; }
}