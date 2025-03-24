using Common.Health.Dtos;

namespace DataIngrestorApi.Core.Health.Abstractions;

public interface IHealthCheckService
{
    Task<HealthCheckResponseDto> HealthCheckAsync();
}