using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using NotificationService.Core.Models;
using NotificationService.Core.Services.Abstractions;

namespace NotificationService.Core.Services;

public class NotificationSender : INotificationSender
{
    private readonly ILogger<NotificationSender> _logger;

    public NotificationSender(ILogger<NotificationSender> logger)
    {
        _logger = logger;
    }

    public async Task SendNotificationAsync(PushNotificationBaseModel notification)
    {
        try
        {
            var fcmMessage = new Message
            {
                Token = notification.Destination,

                Notification = new Notification
                {
                    Title = notification.MessageTittle,
                    Body = notification.MessageBody
                }
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(fcmMessage);

            _logger.LogInformation("Firebase push sent successfully, ID = {result}", result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in FirebaseMessageProducer: {ex.Message}", ex.Message);
        }
    }
}