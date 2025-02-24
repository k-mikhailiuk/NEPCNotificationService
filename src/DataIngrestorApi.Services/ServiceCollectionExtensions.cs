using DataIngrestorApi.Services.Health;
using DataIngrestorApi.Services.Health.Abstractions;
using DataIngrestorApi.Services.MessageProcessor.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngrestorApi.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        => services.AddScoped<IHealthCheckService, HealthCheckService>();
    
    public static IServiceCollection AddMessageProcessor(this IServiceCollection services)
        => services.AddScoped<IMessageProcessor, MessageProcessor.MessageProcessor>();
}