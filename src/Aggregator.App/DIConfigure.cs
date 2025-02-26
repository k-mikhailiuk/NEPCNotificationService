using Aggregator.DataAccess;
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
    }
}