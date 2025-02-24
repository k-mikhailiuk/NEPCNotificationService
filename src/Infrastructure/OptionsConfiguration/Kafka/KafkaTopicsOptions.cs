namespace OptionsConfiguration.Kafka;

public sealed class KafkaTopicsOptions
{
    public const string KafkaTopics = "KafkaTopics"; 
    
    public string HealthCheckTopic { get; set; }
    
    public string NepcTopic { get; set; }
}