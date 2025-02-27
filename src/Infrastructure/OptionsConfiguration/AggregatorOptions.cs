namespace OptionsConfiguration;

public class AggregatorOptions
{
    public const string Aggregator = nameof(Aggregator); 
    
    public int BatchSize { get; set; }
    
    public int IntervalInSeconds { get; set; }
}