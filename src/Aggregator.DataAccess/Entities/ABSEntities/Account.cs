namespace Aggregator.DataAccess.Entities.ABSEntities;

/// <summary>
/// Сущность «Счет» — хранит сведения о банковском счете клиента.
/// </summary>
public class Account
{
    /// <summary>
    /// Номер счета (PK).
    /// </summary>
    public string AccountNo { get; set; } = null!;

    /// <summary>
    /// Код валюты счета.
    /// </summary>
    public int CurrencyID { get; set; }

    /// <summary>
    /// Идентификатор клиента-владельца счета.
    /// </summary>
    public int CustomerID { get; set; }

    /// <summary>
    /// Идентификатор офиса (филиала), к которому привязан счет.
    /// </summary>
    public int OfficeID { get; set; }

    /// <summary>
    /// Группа баланса (Balance Group).
    /// </summary>
    public string BalanceGroup { get; set; } = null!;

    /// <summary>
    /// Наименование счета.
    /// </summary>
    public string AccountName { get; set; } = null!;

    /// <summary>
    /// Дата открытия счета.
    /// </summary>
    public DateTime OpenDate { get; set; }

    /// <summary>
    /// Дата окончания срока действия счета.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Дата закрытия счета.
    /// </summary>
    public DateTime? CloseDate { get; set; }

    /// <summary>
    /// Тип счета (код).
    /// </summary>
    public byte AccountTypeID { get; set; }

    /// <summary>
    /// Текущий баланс в валюте счета.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Текущий баланс в национальной валюте.
    /// </summary>
    public decimal CurrentNationalBalance { get; set; }

    /// <summary>
    /// Дополнительное кодовое имя счета.
    /// </summary>
    public string? Codename { get; set; }

    /// <summary>
    /// Группа счета (Account Group).
    /// </summary>
    public string AccountGroup { get; set; } = null!;

    /// <summary>
    /// Общая сумма по дебету в валюте счета.
    /// </summary>
    public decimal DtSumV { get; set; }

    /// <summary>
    /// Общая сумма по дебету в национальной валюте.
    /// </summary>
    public decimal DtSumN { get; set; }

    /// <summary>
    /// Общая сумма по кредиту в валюте счета.
    /// </summary>
    public decimal CtSumV { get; set; }

    /// <summary>
    /// Общая сумма по кредиту в национальной валюте.
    /// </summary>
    public decimal CtSumN { get; set; }
}