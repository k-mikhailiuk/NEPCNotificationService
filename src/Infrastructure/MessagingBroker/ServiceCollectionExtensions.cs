using MessagingBroker.Abstractions;
using MessagingBroker.Config;
using MessagingBroker.Config.Abstractions;
using MessagingBroker.HealthCheck;
using MessagingBroker.HealthCheck.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingBroker;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProducer<TProducer>(this IServiceCollection services) where TProducer : class, IMessageProducer
        => services.AddSingleton<IMessageProducer, TProducer>();
    
    public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services) where TConsumer : class, IMessageConsumer
        => services.AddSingleton<IMessageConsumer, TConsumer>();

    public static IServiceCollection AddProducerHealthCheck(this IServiceCollection services)
        => services.AddScoped<IKafkaProducerHealthCheck, KafkaProducerHealthCheck>();

    public static IServiceCollection AddKafkaConfigProvider(this IServiceCollection services)
        => services.AddTransient<IKafkaConfigProvider, KafkaConfigProvider>();
}