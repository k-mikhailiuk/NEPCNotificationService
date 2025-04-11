using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngrestorApi.DataAccess;

/// <summary>
/// Методы-расширения для регистрации <see cref="IngressApiDbContext"/> в контейнере зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует <see cref="IngressApiDbContext"/> с использованием строки подключения из конфигурации.
    /// Использует SQL Server в качестве провайдера базы данных.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую добавляется контекст БД.</param>
    /// <param name="configuration">Конфигурация приложения, содержащая строку подключения с ключом "DefaultConnection".</param>
    /// <returns>Обновлённая коллекция сервисов с зарегистрированным <see cref="IngressApiDbContext"/>.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если строка подключения отсутствует.</exception>
    public static IServiceCollection AddIngressApiDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IngressApiDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string is missing");
            
            options.UseSqlServer(connectionString);
        });
        
        return services;
    }
}