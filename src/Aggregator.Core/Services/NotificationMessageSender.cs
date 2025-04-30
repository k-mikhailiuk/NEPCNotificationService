using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Services;

/// <summary>
/// Сервис для отправки push-уведомлений через Firebase Cloud Messaging (FCM).
/// </summary>
public class NotificationMessageSender(IServiceProvider serviceProvider, ILogger<NotificationMessageSender> logger) : INotificationMessageSender
{
    /// <inheritdoc/>
    public async Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

            var tokens = await context.Database
                .SqlQuery<string>(
                    $"SELECT Token FROM PushNotification.NotificationTokens WHERE CustomerID = {message.CustomerId}")
                .ToListAsync(cancellationToken);

            if (tokens.Count == 0)
            {
                return false;
            }

            return await SendMessageAsync(message, tokens, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
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
    private static async Task<bool> SendMessageAsync(NotificationMessage message, IEnumerable<string> tokens,
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

            if (string.IsNullOrEmpty(result))
                return false;
        }

        return true;
    }
}