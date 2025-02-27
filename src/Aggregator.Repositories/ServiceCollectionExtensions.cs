using Aggregator.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Aggregator.Repositories;

public static class ServiceCollectionExtensions
{
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