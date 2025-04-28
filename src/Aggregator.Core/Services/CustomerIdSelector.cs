using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services;

public class CustomerIdSelector : ICustomerIdSelector
{
    /// <inheritdoc />
    public async Task<int?> GetCustomerIdAsync(string accountId, AggregatorDbContext context,
        CancellationToken cancellationToken)
    {
        var aсcountNo = ParseAccountNo(accountId, accountId[..3]);

        return await context.Database
            .SqlQuery<int?>($"SELECT CustomerID AS [Value] FROM dbo.Accounts WHERE AccountNo = {aсcountNo}")
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<Dictionary<string, int?>> GetCustomerIdsAsync(
        IEnumerable<string> accountIds,
        AggregatorDbContext context,
        CancellationToken cancellationToken = default)
    {
        var map = accountIds
            .Distinct()
            .Select(acc => new
            {
                Raw = acc,
                Cleaned = ParseAccountNo(acc, acc[..3])
            })
            .ToList();

        var cleanNumbers = map
            .Select(x => x.Cleaned)
            .Distinct()
            .ToList();

        var inClause = string.Join(", ", cleanNumbers.Select(n => $"'{n}'"));

        FormattableString sql =
            $"SELECT AccountNo, CustomerID FROM dbo.Accounts WHERE AccountNo IN ({inClause})";

        var results = await context.Database
            .SqlQuery<AccountCustomerMap>(sql)
            .ToListAsync(cancellationToken);

        return map.ToDictionary(
            x => x.Raw,
            x => results
                .FirstOrDefault(r => r.AccountNo == x.Cleaned)
                ?.CustomerID
        );
    }

    /// <summary>
    /// Распарсить счет
    /// </summary>
    /// <param name="accountNumber">Номер счета в ПЦ</param>
    /// <param name="bankCode">Код банка в ПЦ</param>
    public string ParseAccountNo(string accountNumber, string bankCode)
    {
        bankCode = bankCode.TrimStart('0');
        var accountNo = accountNumber.StartsWith(bankCode)
            ? accountNumber[bankCode.Length..]
            : accountNumber;

        if (!int.TryParse(accountNo.AsSpan(accountNo.Length - 3, 3), out _))
            throw new Exception("Не удалось получить валюту счета: " + accountNumber);

        return accountNo[..^3];
    }
}