using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControlPanel.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddControlPanelDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ControlPanelDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string is missing");

            options.UseSqlServer(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddIdentityDataSeeder(this IServiceCollection services)
    {
        services.AddTransient<IdentityDataSeeder>();

        return services;
    }
}