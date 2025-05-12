namespace Aggregator.DataAccess.Entities.ABSEntities;

/// <summary>
/// Сущность «Офис» (устройство/терминал) для настроек ABS.
/// </summary>
public class Office
{
    /// <summary>
    /// Сущность «Офис» (устройство/терминал) для настроек ABS.
    /// </summary>
    public string DeviceCode { get; set; } = null!;

    /// <summary>
    /// Уникальный код устройства или терминала.
    /// </summary>
    public int MainOfficeID { get; set; }

    /// <summary>
    /// Код типа устройства.
    /// </summary>
    public int DeviceTypeID { get; set; } = 1;

    /// <summary>
    /// Адрес расположения устройства.
    /// </summary>
    public string? Adress { get; set; }

    /// <summary>
    /// Наименование устройства.
    /// </summary>
    public string? NameDevice { get; set; }

    /// <summary>
    /// Транзитный номер счета.
    /// </summary>
    public string? AccountNoTransit { get; set; }

    /// <summary>
    /// Номер счета для входящих платежей.
    /// </summary>
    public string? AccountNoIncome { get; set; }

    /// <summary>
    /// Внутренний идентификатор офиса (если вложенный).
    /// </summary>
    public int? OfficeIDIn { get; set; }

    /// <summary>
    /// Идентификатор города.
    /// </summary>
    public int CityID { get; set; }

    /// <summary>
    /// Дополнительный код типа устройства.
    /// </summary>
    public int? AdditionalDeviceType { get; set; }

    /// <summary>
    /// Серийный номер устройства.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Географическая широта расположения устройства.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Географическая долгота расположения устройства.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Идентификатор пользовательского процесса.
    /// </summary>
    public int? CustomProcess { get; set; }

    /// <summary>
    /// MCC-код торговой категории.
    /// </summary>
    public string? MccCode { get; set; }

    /// <summary>
    /// Размер комиссии за операцию.
    /// </summary>
    public decimal Comission { get; set; }

    /// <summary>
    /// Общая сумма комиссии.
    /// </summary>
    public decimal TotalComission { get; set; }

    /// <summary>
    /// Партнерская комиссия.
    /// </summary>
    public decimal FriendCommission { get; set; }

    /// <summary>
    /// Сумма кэшбэка.
    /// </summary>
    public decimal CashBack { get; set; }

    /// <summary>
    /// Плановое количество операций за месяц.
    /// </summary>
    public decimal OperationMonthPlan { get; set; }

    /// <summary>
    /// Тип начисления комиссии.
    /// </summary>
    public byte ComisionType { get; set; }

    /// <summary>
    /// Идентификатор типа страхового покрытия.
    /// </summary>
    public int? InsuranceСoverageTypeID { get; set; }

    /// <summary>
    /// Идентификатор типа пандуса устройства.
    /// </summary>
    public int? DeviceRampTypeID { get; set; }

    /// <summary>
    /// Идентификатор типа установки устройства.
    /// </summary>
    public int? DeviceInstallationTypeID { get; set; }

    /// <summary>
    /// Идентификатор типа расположения устройства.
    /// </summary>
    public int? DeviceLocationTypeID { get; set; }

    /// <summary>
    /// Идентификатор типа сенсорного ввода.
    /// </summary>
    public int? DeviceTouchTypingTypeID { get; set; }

    /// <summary>
    /// Идентификатор типа аудиовыхода устройства.
    /// </summary>
    public int? DeviceAudioOutputTypeID { get; set; }

    /// <summary>
    /// Дата установки устройства.
    /// </summary>
    public DateTime? InstallationDate { get; set; }

    /// <summary>
    /// Время начала рабочего дня устройства.
    /// </summary>
    public TimeSpan? StartTime { get; set; }

    /// <summary>
    /// Время окончания рабочего дня устройства.
    /// </summary>
    public TimeSpan? FinishTime { get; set; }

    /// <summary>
    /// Идентификатор производителя устройства.
    /// </summary>
    public int? DeviceProducerID { get; set; }

    /// <summary>
    /// Идентификатор операционной системы.
    /// </summary>
    public int? OperationSystemID { get; set; }

    /// <summary>
    /// Идентификатор метода обработки данных.
    /// </summary>
    public int? DataProcessingMethodID { get; set; }

    /// <summary>
    /// Идентификатор модели устройства.
    /// </summary>
    public int? DeviceModelID { get; set; }

    /// <summary>
    /// Идентификатор типа принадлежности устройства.
    /// </summary>
    public int? DeviceAffiliationTypeID { get; set; }

    /// <summary>
    /// Описание местоположения устройства.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Владелец устройства.
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// Дата вывода устройства из эксплуатации.
    /// </summary>
    public DateTime? CloseDate { get; set; }

    /// <summary>
    /// Идентификатор типа устройства RBO.
    /// </summary>
    public int? RBODeviceTypeID { get; set; }
}