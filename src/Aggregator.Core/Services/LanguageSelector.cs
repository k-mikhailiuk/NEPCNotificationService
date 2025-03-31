using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

public class LanguageSelector : ILanguageSelector
{
    private readonly IServiceProvider _serviceProvider;

    public LanguageSelector(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<long?> GetLanguageId(long customerId,
        CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();
        
        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        await using var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText = @"
            SELECT pns.LanguageId
            FROM PushNotification.Settings pns
            WHERE pns.CustomerID = @customerId";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@customerId";
        parameter.Value = customerId;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result == null || result == DBNull.Value)
            return null;

        return Convert.ToByte(result);
    }
}