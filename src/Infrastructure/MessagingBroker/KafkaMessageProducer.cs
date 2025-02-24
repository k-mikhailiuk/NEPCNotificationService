using Confluent.Kafka;
using MessagingBroker.Abstractions;
using MessagingBroker.Config.Abstractions;
using Microsoft.Extensions.Logging;

namespace MessagingBroker;

public sealed class KafkaMessageProducer : IMessageProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaMessageProducer> _logger;

    public KafkaMessageProducer(ILogger<KafkaMessageProducer> logger, IKafkaConfigProvider kafkaConfigProvider)
    {
        _logger = logger;
        _producer = CreateProducer(kafkaConfigProvider.CreateConfig());
    }
    
    public async Task ProduceAsync<T>(T obj, string destination,
        CancellationToken token = default)
    {
        try
        {
            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = obj as string ?? throw new InvalidOperationException("Message must be a string")
            };
            
            var deliveryResult = await _producer.ProduceAsync(destination, message, token);
            
            if (deliveryResult.Status == PersistenceStatus.Persisted)
            {
                _logger.LogInformation("Message delivered to {TopicPartitionOffset}", deliveryResult.TopicPartitionOffset);
            }
            else
            {
                _logger.LogWarning("Message not guaranteed persist: {Status}", deliveryResult.Status);
            }
				
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError("Error during send message. {ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during send message. {ex}", ex);
            throw;
        }
    }

    private IProducer<string, string> CreateProducer(ProducerConfig config)
    {
        return new ProducerBuilder<string, string>(config)
            .SetErrorHandler((_, e) =>
            {
                _logger.LogError("Kafka Producer error: {Reason}", e.Reason);
            })
            .SetStatisticsHandler((_, json) =>
            {
                _logger.LogDebug("Kafka Producer stats: {Json}", json);
            })
            .Build();
    }
}