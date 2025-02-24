using DataIngrestorApi.App.Extensions;
using DataIngrestorApi.Services;
using MessagingBroker;
using OptionsConfiguration;

namespace DataIngrestorApi.App;

/// <summary>
/// Di configuration for ingress api
/// </summary>
public static class DIConfigure
{
    
    /// <summary>
    /// Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwagger();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddProducer<KafkaMessageProducer>();
        services.AddHealthCheck();
        services.AddProducerHealthCheck();
        services.AddKafkaMessageProducerOptions(configuration);
        services.AddKafkaTopics(configuration);
        services.AddKafkaConfigProvider();
        services.AddMessageProcessor();
    }
}