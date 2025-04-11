using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.DataAccess.Abstractions;
using Scrutor;

namespace NotificationService.DataAccess;

/// <summary>
/// Методы-расширения для регистрации DbContext и репозиториев в контейнере зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует <see cref="NotificationServiceDbContext"/> с использованием строки подключения из конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения, содержащая строку подключения.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
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
    
    /// <summary>
    /// Регистрирует <see cref="IUnitOfWork"/> и все репозитории, реализованные в сборке <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddNotificationServiceRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.Scan(scan => scan
            .FromAssemblyOf<IUnitOfWork>()
            .AddClasses(classes => classes.InNamespaces("NotificationService.DataAccess"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}