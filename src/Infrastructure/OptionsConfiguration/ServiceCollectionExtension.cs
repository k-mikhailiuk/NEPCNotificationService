using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionsConfiguration;

/// <summary>
/// Методы-расширения для регистрации конфигурационных опций в контейнере зависимостей.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Регистрирует опции агрегатора <see cref="AggregatorOptions"/> из конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddAggregatorOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<AggregatorOptions>(configuration.GetSection(AggregatorOptions.Aggregator));
    }

    /// <summary>
    /// Регистрирует строку подключения к базе данных как конфигурационную строку.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если строка подключения отсутствует.</exception>
    public static IServiceCollection AddDatabaseConnectionString(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string is missing");

        return services.Configure<string>(options => options = connectionString);
    }

    /// <summary>
    /// Регистрирует опции для процессора уведомлений <see cref="NotificationProcessorOptions"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddNotificationProcessorOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<NotificationProcessorOptions>(
            configuration.GetSection(NotificationProcessorOptions.NotificationProcessor));
    }

    /// <summary>
    /// Регистрирует опции текстов сообщений уведомлений <see cref="NotificationMessageOptions"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddNotificationMessageOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<NotificationMessageOptions>(
            configuration.GetSection(NotificationMessageOptions.NotificationMessage));
    }
    
    /// <summary>
    /// Регистрирует опции учётной записи администратора <see cref="AdminUserOptions"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddAdminUserOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<AdminUserOptions>(
            configuration.GetSection(AdminUserOptions.AdminUser));
    }
}