using Aggregator.Core.Services.Abstractions;

namespace Aggregator.Core.Services;

public class AccountNoParser : IAccountNoParser
{
    /// <summary>
    /// Распарсить счет
    /// </summary>
    /// <param name="accountNumber">Номер счета в ПЦ</param>
    public string ParseAccountNo(string accountNumber)
    {
        var bankCode = accountNumber[..3];
        
        bankCode = bankCode.TrimStart('0');
        var accountNo = accountNumber.StartsWith(bankCode)
            ? accountNumber[bankCode.Length..]
            : accountNumber;
        
        return accountNo.Contains('=') ? accountNo[..^8] : accountNo[..^3];
    }
}