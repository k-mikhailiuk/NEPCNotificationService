using Common.Health.Dtos;
using DataIngrestorApi.Services.Health;
using DataIngrestorApi.Services.Health.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DataIngrestorApi.App.Controllers;

[ApiController]
[Route("_hc")]
public class HealthController : ControllerBase
{
    private readonly IHealthCheckService _healthCheckService;
    
    public HealthController(IHealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<HealthCheckResponseDto> HealthCheck()
    {
        return await _healthCheckService.HealthCheckAsync();
    }
}