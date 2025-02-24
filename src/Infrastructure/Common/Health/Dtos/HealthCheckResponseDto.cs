namespace Common.Health.Dtos;

public class HealthCheckResponseDto
{
    public HealthCheckResponseDto()
    {
        Timestamp = DateTimeOffset.UtcNow;
        Components = new Dictionary<string, HealthCheckComponentDto>();
    }
    
    public DateTimeOffset Timestamp { get; set; }
    
    public IDictionary<string, HealthCheckComponentDto> Components { get; set; }
}