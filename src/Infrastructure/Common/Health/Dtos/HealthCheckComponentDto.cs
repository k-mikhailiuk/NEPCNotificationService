namespace Common.Health.Dtos;

public class HealthCheckComponentDto
{
    public double RequestTime { get; set; }
    
    public string Name { get; set; }
    
    public bool Status { get; set; }
    
}