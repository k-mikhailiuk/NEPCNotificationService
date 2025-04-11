using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services;

/// <summary>
/// Реализация интерфейса <see cref="ILanguageSelector"/> для получения идентификатора языка на основе идентификатора клиента.
/// </summary>
public class LanguageSelector : ILanguageSelector
{
    /// <summary>
    /// Асинхронно получает идентификатор языка для заданного клиента.
    /// </summary>
    /// <param name="customerId">Идентификатор клиента.</param>
    /// <param name="context">Контекст базы данных <see cref="AggregatorDbContext"/>.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая идентификатор языка как значение <see cref="long"/>, или null, если язык не найден.
    /// </returns>
    public async Task<long?> GetLanguageIdAsync(long customerId, AggregatorDbContext context,
        CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();
        
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