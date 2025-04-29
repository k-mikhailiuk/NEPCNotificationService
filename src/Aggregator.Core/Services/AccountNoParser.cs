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

        if (!int.TryParse(accountNo.AsSpan(accountNo.Length - 3, 3), out _))
            throw new Exception("Не удалось получить валюту счета: " + accountNumber);

        return accountNo[..^3];
    }
}