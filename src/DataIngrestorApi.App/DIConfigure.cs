using DataIngrestorApi.App.Extensions;
using DataIngrestorApi.DataAccess;
using DataIngrestorApi.Services.Extensions;
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
        services.AddJsonSerializeOptions();
        services.AddSwagger();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthCheck();
        services.AddMessageProcessor();
        services.AddDatabaseConnectionString(configuration);
        services.AddDbContext(configuration);
    }

    // private static void RegisterKafka(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddKafkaConfigProvider();
    //     services.AddProducer<KafkaMessageProducer>();
    //     services.AddKafkaTopics(configuration);
    //     services.AddKafkaMessageProducerOptions(configuration);
    //     services.AddProducerHealthCheck();
    // }
}