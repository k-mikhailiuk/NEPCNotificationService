namespace OptionsConfiguration;

public class NotificationAviabilityOptions
{
    public const string NotificationAviability = nameof(NotificationAviability);

    public bool IssFinAuthAviable { get; set; }
    public bool AcqFinAuthAviable { get; set; }
    public bool CardStatusChangeAviable { get; set; }
    public bool PinChangeAviable { get; set; }
    public bool TokenStatusChangeAviable { get; set; }
    public bool UnholdAviable { get; set; }
    public bool OwiUserActionAviable { get; set; }
    public bool AcctBalChangeAviable { get; set; }
}