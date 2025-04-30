namespace Aggregator.DataAccess.Entities.ABSEntities;

public class Account
{
    public string AccountNo { get; set; } = null!;

    public int CurrencyID { get; set; }

    public int CustomerID { get; set; }

    public int OfficeID { get; set; }

    public string BalanceGroup { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public DateTime OpenDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CloseDate { get; set; }

    public byte AccountTypeID { get; set; }

    public decimal CurrentBalance { get; set; }

    public decimal CurrentNationalBalance { get; set; }

    public string? Codename { get; set; }

    public string AccountGroup { get; set; } = null!;

    public decimal DtSumV { get; set; }

    public decimal DtSumN { get; set; }

    public decimal CtSumV { get; set; }

    public decimal CtSumN { get; set; }
}