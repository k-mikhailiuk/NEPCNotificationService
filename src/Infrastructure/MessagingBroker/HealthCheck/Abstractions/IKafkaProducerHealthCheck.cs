using Common.Health.Dtos;

namespace MessagingBroker.HealthCheck.Abstractions;

public interface IKafkaProducerHealthCheck
{
    Task<HealthCheckComponentDto> HealthCheckAsync();
}