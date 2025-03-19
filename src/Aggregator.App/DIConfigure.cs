using Aggregator.Core;
using Aggregator.DataAccess;
using Aggregator.Repositories;
using OptionsConfiguration;

namespace Aggregator.App;

public static class DIConfigure
{
    /// <summary>
    /// Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionString(configuration);
        services.AddAggregatorDbContext(configuration);
        services.AddRepositories();
        services.AddCommands();
        services.AddFactories();
        services.AddAggregatorOptions(configuration);
        services.AddBehaviors();
        services.AddValidators();
        services.AddMappers();
        services.AddNotificationMessageBuilders();
        services.AddNotificationMessageOptions(configuration);
    }
}