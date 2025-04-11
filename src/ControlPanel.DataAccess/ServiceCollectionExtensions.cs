using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.DataSeeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace ControlPanel.DataAccess;

/// <summary>
/// Методы расширения для регистрации компонентов DataAccess в DI-контейнере.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует контекст базы данных ControlPanel с использованием SQL Server.
    /// </summary>
    /// <param name="services">Коллекция сервисов DI.</param>
    /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
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

    /// <summary>
    /// Регистрирует классы для начальной загрузки данных.
    /// </summary>
    /// <param name="services">Коллекция сервисов DI.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        services.AddTransient<IdentityDataSeeder>();
        services.AddTransient<KeyWordsDataSeeder>();
        services.AddTransient<NotificationMessageTextDirectoriesDataSeeder>();

        return services;
    }
    
    /// <summary>
    /// Регистрирует репозитории и единицу работы с использованием сканирования сборки.
    /// </summary>
    /// <param name="services">Коллекция сервисов DI.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
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