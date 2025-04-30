using Aggregator.Core;
using Aggregator.DataAccess;
using ControlPanel.DataAccess;
using OptionsConfiguration;

namespace Aggregator.App;

/// <summary>
/// Register services
/// </summary>
public static class DIConfigure
{
    /// <summary>
    /// Register custom services
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будут зарегистрированы зависимости.</param>
    /// <param name="configuration">Конфигурация приложения, которая используется для настройки сервисов.</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionString(configuration);
        services.AddAggregatorDbContext(configuration);
        services.AddRepositories();
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