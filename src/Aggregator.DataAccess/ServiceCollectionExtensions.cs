using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess;

/// <summary>
/// Расширения для <see cref="IServiceCollection"/> для регистрации контекста базы данных Aggregator.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует контекст базы данных <see cref="AggregatorDbContext"/> в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
    /// <returns>Обновлённая коллекция сервисов с зарегистрированным контекстом базы данных.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если строка подключения отсутствует в конфигурации.</exception>
    public static IServiceCollection AddAggregatorDbContext(this IServiceCollection services,
        IConfiguration configuration)
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