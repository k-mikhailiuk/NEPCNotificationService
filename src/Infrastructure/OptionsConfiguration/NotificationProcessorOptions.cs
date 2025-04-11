namespace OptionsConfiguration;

/// <summary>
/// Класс конфигурации параметров процессора уведомлений.
/// Используется для настройки интервалов обработки и пути к файлу учётных данных Firebase.
/// </summary>
public class NotificationProcessorOptions
{
    /// <summary>
    /// Имя секции конфигурации.
    /// </summary>
    public const string NotificationProcessor = nameof(NotificationProcessor); 
    
    /// <summary>
    /// Интервал между циклами обработки уведомлений в секундах.
    /// </summary>
    public int IntervalInSeconds { get; set; }
    
    /// <summary>
    /// Путь к файлу с учётными данными Firebase (обычно .json).
    /// </summary>
    public string FirebaseCredentialsFilePath { get; set; }
}