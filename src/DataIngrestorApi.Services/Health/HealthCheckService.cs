using Common.Health.Dtos;
using DataIngrestorApi.Services.Health.Abstractions;
using MessagingBroker.HealthCheck.Abstractions;

namespace DataIngrestorApi.Services.Health;

public class HealthCheckService : IHealthCheckService
{
    // private readonly IKafkaProducerHealthCheck _kafkaProducerHealthCheck;

    // public HealthCheckService(IKafkaProducerHealthCheck kafkaProducerHealthCheck)
    // {
    //     _kafkaProducerHealthCheck = kafkaProducerHealthCheck;
    // }
    public async Task<HealthCheckResponseDto> HealthCheckAsync()
    {
        var response =  new HealthCheckResponseDto();

        //await HealthCheckKafkaAsync(response);

        return response;
    }

    // private async Task<HealthCheckResponseDto> HealthCheckKafkaAsync(HealthCheckResponseDto responseDto)
    // {
    //     var kafka = await _kafkaProducerHealthCheck.HealthCheckAsync();
    //     responseDto.Components.Add(kafka.Name, kafka);
    //     
    //     return responseDto;
    // }
}