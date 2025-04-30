namespace Aggregator.DataAccess.Entities.ABSEntities;

public class Office
{
    public string DeviceCode { get; set; } = null!;

    public int MainOfficeID { get; set; }

    public int DeviceTypeID { get; set; } = 1;

    public string? Adress { get; set; }

    public string? NameDevice { get; set; }

    public string? AccountNoTransit { get; set; }

    public string? AccountNoIncome { get; set; }

    public int? OfficeIDIn { get; set; }

    public int CityID { get; set; }

    public int? AdditionalDeviceType { get; set; }

    public string? SerialNumber { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int? CustomProcess { get; set; }

    public string? MccCode { get; set; }

    public decimal Comission { get; set; }

    public decimal TotalComission { get; set; }

    public decimal FriendCommission { get; set; }

    public decimal CashBack { get; set; }

    public decimal OperationMonthPlan { get; set; }

    public byte ComisionType { get; set; }

    public int? InsuranceСoverageTypeID { get; set; }
    public int? DeviceRampTypeID { get; set; }
    public int? DeviceInstallationTypeID { get; set; }
    public int? DeviceLocationTypeID { get; set; }
    public int? DeviceTouchTypingTypeID { get; set; }
    public int? DeviceAudioOutputTypeID { get; set; }

    public DateTime? InstallationDate { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? FinishTime { get; set; }

    public int? DeviceProducerID { get; set; }

    public int? OperationSystemID { get; set; }

    public int? DataProcessingMethodID { get; set; }

    public int? DeviceModelID { get; set; }

    public int? DeviceAffiliationTypeID { get; set; }

    public string? Location { get; set; }

    public string? Owner { get; set; }

    public DateTime? CloseDate { get; set; }

    public int? RBODeviceTypeID { get; set; }
}