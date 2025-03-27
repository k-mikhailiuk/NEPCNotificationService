using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationService.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServiceDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationServiceDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string is missing");
            
            options.UseSqlServer(connectionString);
        });
        
        return services;
    }
}