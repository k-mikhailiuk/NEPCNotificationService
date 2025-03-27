using Aggregator.DataAccess;
using Aggregator.Repositories;
using NotificationService.Core;
using NotificationService.DataAccess;
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
        services.AddNotificationServiceDbContext(configuration);
        services.AddMessageSender();
        services.AddNotificationProcessorOptions(configuration);
    }
}
