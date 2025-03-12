using Aggregator.DataAccess;
using Aggregator.Repositories;
using NotificationService.Core;
using OptionsConfiguration;

namespace NotificationService.App;

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
        services.AddMessageSender();
    }
}
