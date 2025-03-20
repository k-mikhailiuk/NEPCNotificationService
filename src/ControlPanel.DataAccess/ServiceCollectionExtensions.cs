using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.DataSeeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

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

    public static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        services.AddTransient<IdentityDataSeeder>();
        services.AddTransient<KeyWordsDataSeeder>();
        services.AddTransient<NotificationMessageTextDirectoriesDataSeeder>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.Scan(scan => scan
            .FromAssemblyOf<IUnitOfWork>()
            .AddClasses(classes => classes.InNamespaces("ControlPanel.DataAccess.Repositories"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}