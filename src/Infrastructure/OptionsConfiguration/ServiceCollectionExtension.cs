using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptionsConfiguration.Kafka;

namespace OptionsConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddKafkaMessageProducerOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<KafkaProducerOptions>(configuration.GetSection(KafkaProducerOptions.KafkaProducer));
    }

    public static IServiceCollection AddKafkaMessageConsumerOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<KafkaConsumerOptions>(configuration.GetSection(KafkaConsumerOptions.KafkaConsumer));
    }

    public static IServiceCollection AddKafkaTopics(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<KafkaTopicsOptions>(configuration.GetSection(KafkaTopicsOptions.KafkaTopics));
    }
}