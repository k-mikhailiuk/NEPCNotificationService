using Confluent.Kafka;
using MessagingBroker.Config.Abstractions;
using Microsoft.Extensions.Options;
using OptionsConfiguration.Kafka;

namespace MessagingBroker.Config;

public class KafkaConfigProvider : IKafkaConfigProvider
{
    private readonly KafkaProducerOptions _producerOptions;
    
    public KafkaConfigProvider(IOptions<KafkaProducerOptions> options)
    {
        _producerOptions = options.Value;
    }

    public ProducerConfig CreateConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = _producerOptions.BootstrapServers,
            Acks = Acks.All,
            EnableIdempotence = false
        };
    }
}