using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

public class CurrencyReplacer(IServiceProvider serviceProvider) : ICurrencyReplacer
{
    public async Task<string?> ReplaceCurrency(string currency, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(currency))
            return string.Empty;
        
        using var scope = serviceProvider.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText = @"
                SELECT currencies.Symbol
                FROM dbo.Currencies currencies 
                WHERE CurrencyID = @currencyID";

        
        var parameter = command.CreateParameter();
        parameter.ParameterName = "@currencyID";
        parameter.Value = currency;
        command.Parameters.Add(parameter);

        var symbol = await command.ExecuteScalarAsync(cancellationToken);
        
        return symbol is null ? string.Empty : symbol.ToString();
    }
}