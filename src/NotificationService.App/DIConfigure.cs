using NotificationService.Core;
using NotificationService.DataAccess;
using OptionsConfiguration;

namespace NotificationService.App;

/// <summary>
/// Класс конфигурации зависимостей для NotificationService.
/// Содержит методы регистрации сервисов, контекстов БД, репозиториев и конфигурационных опций.
/// </summary>
public static class DIConfigure
{
    /// <summary>
    /// Регистрирует пользовательские сервисы, контекст базы данных и параметры конфигурации
    /// для модуля отправки уведомлений.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionString(configuration);
        services.AddNotificationServiceDbContext(configuration);
        services.AddNotificationServiceRepositories();
        services.AddNotificationMessageServices();
        services.AddNotificationProcessorOptions(configuration);
    }
}
