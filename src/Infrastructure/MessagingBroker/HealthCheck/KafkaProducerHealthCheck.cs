using System.Diagnostics;
using Common.Health.Dtos;
using MessagingBroker.Abstractions;
using MessagingBroker.HealthCheck.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsConfiguration.Kafka;

namespace MessagingBroker.HealthCheck;

public class KafkaProducerHealthCheck : IKafkaProducerHealthCheck
{
    private readonly IMessageProducer _producer;
    private readonly ILogger<KafkaProducerHealthCheck> _logger;
    private readonly KafkaTopicsOptions _kafkaTopics;

    public KafkaProducerHealthCheck(
        IMessageProducer producer,
        ILogger<KafkaProducerHealthCheck> logger,
        IOptions<KafkaTopicsOptions> kafkaTopics)
    {
        _producer = producer;
        _logger = logger;
        _kafkaTopics = kafkaTopics.Value;
    }
    
    public async Task<HealthCheckComponentDto> HealthCheckAsync()
    {
        var watch = Stopwatch.StartNew();
        var status = await KafkaCheckAsync();
        watch.Stop();

        return new HealthCheckComponentDto
        {
            Name = "kafka",
            Status = status.Status == HealthStatus.Healthy,
            RequestTime = watch.ElapsedMilliseconds / 1000.0
        };
    }

    private async Task<HealthCheckResult> KafkaCheckAsync()
    {
        try
        {
            var task = _producer.ProduceAsync(
                obj: $"Producer health check {DateTimeOffset.UtcNow}", 
                destination: _kafkaTopics.HealthCheckTopic
            );

            var completedTask = await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(2)));

            if (completedTask == task)
            {
                await task;
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Degraded();
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured during kafka health check: {e}", e);
            return HealthCheckResult.Unhealthy();
        }
    }
}