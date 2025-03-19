using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptionsConfiguration.Kafka;

namespace OptionsConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddKafkaMessageProducerOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<KafkaProducerOptions>(configuration.GetSection(KafkaProducerOptions.KafkaProducer));
    }

    public static IServiceCollection AddKafkaMessageConsumerOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<KafkaConsumerOptions>(configuration.GetSection(KafkaConsumerOptions.KafkaConsumer));
    }

    public static IServiceCollection AddKafkaTopics(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<KafkaTopicsOptions>(configuration.GetSection(KafkaTopicsOptions.KafkaTopics));
    }

    public static IServiceCollection AddAggregatorOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<AggregatorOptions>(configuration.GetSection(AggregatorOptions.Aggregator));
    }

    public static IServiceCollection AddDatabaseConnectionString(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string is missing");

        return services.Configure<string>(options => options = connectionString);
    }

    public static IServiceCollection AddNotificationProcessorOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<NotificationProcessorOptions>(
            configuration.GetSection(NotificationProcessorOptions.NotificationProcessor));
    }

    public static IServiceCollection AddNotificationMessageOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Configure<NotificationMessageOptions>(
            configuration.GetSection(NotificationMessageOptions.NotificationMessage));
    }
}