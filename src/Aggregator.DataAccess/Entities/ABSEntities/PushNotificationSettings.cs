namespace Aggregator.DataAccess.Entities.ABSEntities;

public class PushNotificationSettings
{
    public int LoginID { get; set; }
    public int CustomerID { get; set; }

    public string? LoginName { get; set; }

    public bool IsActive { get; set; }

    public DateTime LastRequestDate { get; set; }

    public int LanguageID { get; set; }
}