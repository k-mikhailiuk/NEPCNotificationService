using Aggregator.DataAccess;
using Aggregator.Repositories.Extensions;
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
    }
}