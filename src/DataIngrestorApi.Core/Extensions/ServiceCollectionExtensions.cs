using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.Core.Health;
using DataIngrestorApi.Core.Health.Abstractions;
using DataIngrestorApi.Core.MessageProcessor.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngrestorApi.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        => services.AddScoped<IHealthCheckService, HealthCheckService>();
    
    public static IServiceCollection AddMessageProcessor(this IServiceCollection services)
        => services.AddScoped<IMessageProcessor, MessageProcessor.MessageProcessor>();

    public static IServiceCollection AddJsonSerializeOptions(this IServiceCollection services)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        services.AddSingleton(jsonOptions);
        
        return services;
    }
}