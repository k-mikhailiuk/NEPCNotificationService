using System.Data;
using Aggregator.DataAccess.Entities;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Core.Services.Abstractions;
using NotificationService.DataAccess;

namespace NotificationService.Core.Services;

/// <summary>
/// Сервис для отправки push-уведомлений через Firebase Cloud Messaging (FCM).
/// </summary>
public class NotificationMessageSender(IServiceProvider serviceProvider) : INotificationMessageSender
{
    /// <inheritdoc/>
    public async Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            await using var context = scope.ServiceProvider.GetRequiredService<NotificationServiceDbContext>();

            await using var connection = context.Database.GetDbConnection();

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

            var reader = await command.ExecuteReaderAsync(cancellationToken);

            var tokens = new List<string>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var token = reader.GetString(0);
                tokens.Add(token);
            }

            if (tokens.Count == 0)
            {
                return false;
            }

            return await SendMessageAsync(message, tokens, cancellationToken);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// Отправляет сообщение на каждый из указанных токенов FCM.
    /// </summary>
    /// <param name="message">Содержимое уведомления.</param>
    /// <param name="tokens">Список FCM-токенов устройств.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns><c>true</c>, если все отправки завершились успешно; иначе <c>false</c>.</returns>
    private static async Task<bool> SendMessageAsync(NotificationMessage message, List<string> tokens,
        CancellationToken cancellationToken = default)
    {
        foreach (var token in tokens)
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
            
            if(string.IsNullOrEmpty(result))
                return false;
        }
        
        return true;
    }
}