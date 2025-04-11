using Aggregator.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Aggregator.Repositories;

/// <summary>
/// Расширения для регистрации репозиториев в DI-контейнере.
/// </summary>
/// <remarks>
/// Этот класс содержит метод для сканирования сборки и регистрации всех классов, реализующих репозитории,
/// а также регистрацию <see cref="IUnitOfWork"/>.
/// </remarks>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует репозитории и единицу работы в DI-контейнере.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <returns>Обновлённая коллекция сервисов с зарегистрированными репозиториями.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.Scan(scan => scan
            .FromAssemblyOf<IUnitOfWork>()
            .AddClasses(classes => classes.InNamespaces("Aggregator.Repositories.Repositories"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}