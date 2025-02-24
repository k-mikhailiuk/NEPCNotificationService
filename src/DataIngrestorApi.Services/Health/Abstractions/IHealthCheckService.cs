using Common.Health.Dtos;

namespace DataIngrestorApi.Services.Health.Abstractions;

public interface IHealthCheckService
{
    Task<HealthCheckResponseDto> HealthCheckAsync();
}