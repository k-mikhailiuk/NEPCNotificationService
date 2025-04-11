using System.Data;
using System.Data.Common;
using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Core.Services.Abstractions;
using NotificationService.DataAccess;

namespace NotificationService.Core.Services;

/// <summary>
/// Сервис для сохранения истории отправленных push-уведомлений в базу данных.
/// </summary>
public class NotificationHistorySaver(IServiceProvider serviceProvider) : INotificationHistorySaver
{
    /// <inheritdoc/>
    public async Task SaveAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        await using var context = scope.ServiceProvider.GetRequiredService<NotificationServiceDbContext>();

        await using var connection = context.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText = @"
                    INSERT INTO PushNotification.SendHistories
                    (
                        CustomerID,
                        CreateDate,
                        SendDate,
                        NotificationText,
                        NotificationTitle,
                        HasMessageRead,
                        StatusID,
                        IsNews,
                        LoginID
                    )
                    VALUES
                    (
                        @CustomerID,
                        @CreateDate,
                        @SendDate,
                        @NotificationText,
                        @NotificationTitle,
                        @HasMessageRead,
                        @StatusID,
                        @IsNews,
                        @LoginID
                    )";

        var paramCustomerId = command.CreateParameter();
        paramCustomerId.ParameterName = "@CustomerID";
        paramCustomerId.Value = message.CustomerId;
        command.Parameters.Add(paramCustomerId);

        var paramDateTime = command.CreateParameter();
        paramDateTime.ParameterName = "@CreateDate";
        paramDateTime.Value = DateTime.Now;
        command.Parameters.Add(paramDateTime);
        
        var paramSendDate = command.CreateParameter();
        paramSendDate.ParameterName = "@SendDate";
        paramSendDate.Value = DateTime.Now;
        command.Parameters.Add(paramSendDate);

        var paramNotificationText = command.CreateParameter();
        paramNotificationText.ParameterName = "@NotificationText";
        paramNotificationText.DbType = DbType.String;
        paramNotificationText.Size = 500;
        paramNotificationText.Value = message.Message;
        command.Parameters.Add(paramNotificationText);

        var paramNotificationTitle = command.CreateParameter();
        paramNotificationTitle.ParameterName = "@NotificationTitle";
        paramNotificationTitle.Value = message.Title;
        command.Parameters.Add(paramNotificationTitle);

        var paramHasRead = command.CreateParameter();
        paramHasRead.ParameterName = "@HasMessageRead";
        paramHasRead.Value = false;
        command.Parameters.Add(paramHasRead);

        var paramStatusId = command.CreateParameter();
        paramStatusId.ParameterName = "@StatusID";
        paramStatusId.Value = 2;
        command.Parameters.Add(paramStatusId);

        var paramIsNews = command.CreateParameter();
        paramIsNews.ParameterName = "@IsNews";
        paramIsNews.Value = false;
        command.Parameters.Add(paramIsNews);

        var loginId = await GetLoginIdAsync(message.CustomerId, connection, cancellationToken);
        
        if(!loginId.HasValue)
            return;
            
        var paramLoginId = command.CreateParameter();
        paramLoginId.ParameterName = "@LoginID";
        paramLoginId.Value = loginId.Value;
        command.Parameters.Add(paramLoginId);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    /// <summary>
    /// Выполняет асинхронный запрос к базе данных для получения LoginID по CustomerID.
    /// </summary>
    /// <param name="customerId">Идентификатор клиента (CustomerID).</param>
    /// <param name="connection">Открытое подключение к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>LoginID клиента, если найден; иначе <c>null</c>.</returns>
    private static async Task<int?> GetLoginIdAsync(long customerId, DbConnection connection,
        CancellationToken cancellationToken)
    {
        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText = @"
            SELECT pns.LoginID
            FROM PushNotification.Settings pns
            WHERE pns.CustomerID = @customerId";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@customerId";
        parameter.Value = customerId;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result == null || result == DBNull.Value)
            return null;

        return Convert.ToInt32(result);
    }
}