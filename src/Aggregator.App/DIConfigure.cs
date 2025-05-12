using Aggregator.Core;
using Aggregator.DataAccess;
using ControlPanel.DataAccess;
using OptionsConfiguration;

namespace Aggregator.App;

/// <summary>
/// Класс для конфигурации DI контейнера и регистрации сервисов приложения ControlPanel.
/// </summary>
public static class DIConfigure
{
    /// <summary>
    /// Регистрирует кастомные сервисы в DI контейнере
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionString(configuration);
        services.AddAggregatorDbContext(configuration);
        services.AddRepositories();
        services.AddControlPanelDbContext(configuration);
        services.AddControlPanelRepositories();
        services.AddCommands();
        services.AddFactories();
        services.AddAggregatorOptions(configuration);
        services.AddBehaviors();
        services.AddValidators();
        services.AddMappers();
        services.AddBuilders();
        services.AddNotificationMessageOptions(configuration);
        services.AddNotificationMessageServices();
        services.AddNotificationCompositors();
        services.AddNotificationDataLoaders();
        services.AddNotificationProcessorOptions(configuration);
        services.AddServices();
    }
}