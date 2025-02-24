using System.Text.Json;
using DataIngrestorApi.DTOs;
using DataIngrestorApi.Services.MessageProcessor.Abstractions;
using MessagingBroker.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsConfiguration.Kafka;

namespace DataIngrestorApi.Services.MessageProcessor;

public class MessageProcessor : IMessageProcessor
{
    private readonly IMessageProducer _producer;
    private readonly ILogger<MessageProcessor> _logger;
    private readonly KafkaTopicsOptions _kafkaTopics;

    public MessageProcessor(IMessageProducer producer, ILogger<MessageProcessor> logger, IOptions<KafkaTopicsOptions> kafkaTopics)
    {
        _producer = producer;
        _logger = logger;
        _kafkaTopics = kafkaTopics.Value;
    }

    public async Task ProcessBatchAsync(NotificationRequestDto request)
    {
        var tasks = new List<Task>();

        foreach (var batchItem in request.Batch)
        {
            try
            {
                var jsonMessage = JsonSerializer.Serialize(batchItem);

                tasks.Add(_producer.ProduceAsync(jsonMessage, _kafkaTopics.NepcTopic));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process batch item: {BatchItem}", batchItem);
            }
        }

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "One or more messages failed to send to Kafka.");
        }
    }
}