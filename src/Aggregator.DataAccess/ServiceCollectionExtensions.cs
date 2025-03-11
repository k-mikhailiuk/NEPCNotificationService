using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAggregatorDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AggregatorDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string is missing");
            
            options.UseSqlServer(connectionString);
        });
        
        return services;
    }
}