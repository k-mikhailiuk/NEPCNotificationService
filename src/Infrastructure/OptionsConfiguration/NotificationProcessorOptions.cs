namespace OptionsConfiguration;

public class NotificationProcessorOptions
{
    public const string NotificationProcessor = nameof(NotificationProcessor); 
    
    public int IntervalInSeconds { get; set; }
    
    public string FirebaseCredentialsFilePath { get; set; }
}