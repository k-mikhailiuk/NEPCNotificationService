using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionsConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAggregatorOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<AggregatorOptions>(configuration.GetSection(AggregatorOptions.Aggregator));
    }

    public static IServiceCollection AddDatabaseConnectionString(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string is missing");

        return services.Configure<string>(options => options = connectionString);
    }

    public static IServiceCollection AddNotificationProcessorOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<NotificationProcessorOptions>(
            configuration.GetSection(NotificationProcessorOptions.NotificationProcessor));
    }

    public static IServiceCollection AddNotificationMessageOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<NotificationMessageOptions>(
            configuration.GetSection(NotificationMessageOptions.NotificationMessage));
    }
    
    public static IServiceCollection AddAdminUserOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<AdminUserOptions>(
            configuration.GetSection(AdminUserOptions.AdminUser));
    }
}