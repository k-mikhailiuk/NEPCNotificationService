using Confluent.Kafka;

namespace MessagingBroker.Config.Abstractions;

public interface IKafkaConfigProvider
{
    ProducerConfig CreateConfig();
}