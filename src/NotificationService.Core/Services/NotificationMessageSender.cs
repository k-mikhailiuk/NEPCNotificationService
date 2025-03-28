using System.Data;
using Aggregator.DataAccess.Entities;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Core.Services.Abstractions;
using NotificationService.DataAccess;

namespace NotificationService.Core.Services;

public class NotificationMessageSender(IServiceProvider serviceProvider) : INotificationMessageSender
{
    public async Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            await using var context = scope.ServiceProvider.GetRequiredService<NotificationServiceDbContext>();

            var connection = context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();

            command.CommandText = @"
                SELECT tokens.Token
                FROM PushNotification.NotificationTokens tokens 
                WHERE CustomerID = @customerId";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@customerId";
            parameter.Value = message.CustomerId;
            command.Parameters.Add(parameter);

            var result = await command.ExecuteScalarAsync(cancellationToken);

            if (result == null || result == DBNull.Value)
                return false;

            var sendResult = await SendMessageAsync(message, result.ToString(), cancellationToken);

            return !string.IsNullOrEmpty(sendResult);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private static async Task<string> SendMessageAsync(NotificationMessage message, string token,
        CancellationToken cancellationToken = default)
    {
        var fcmMessage = new Message
        {
            Token = token,

            Notification = new Notification
            {
                Title = message.Title,
                Body = message.Message
            }
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(fcmMessage, cancellationToken);

        return result;
    }
}