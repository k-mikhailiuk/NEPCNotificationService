namespace ControlPanel.DataAccess.Entities.Enum;

public enum NotificationMessageType : byte
{
    Undefined = 0,
    IssFinAuth = 1,
    AcqFinAuth = 2,
    CardStatusChange = 3,
    PinChange = 4,
    OwiUserAction = 5,
    Unhold = 6,
    AcctBalChange = 7,
    TokenStatusChange = 8,
    AcsOtp = 9
}