using Aggregator.DataAccess.Entities.Enum;
using Microsoft.Extensions.Options;
using NotificationService.Core.Services.Abstractions;
using NotificationService.DataAccess.Abstractions;
using OptionsConfiguration;

namespace NotificationService.App;

public class NotificationProcessor(
    ILogger<NotificationProcessor> logger,
    IServiceProvider serviceProvider,
    IOptions<NotificationProcessorOptions> notificationProcessorOptions)
    : BackgroundService
{
    private readonly NotificationProcessorOptions _notificationProcessorOptions = notificationProcessorOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken cancelationToken)
    {
        logger.LogInformation("Notification processor started");

        FirebaseInitializer.Init(_notificationProcessorOptions.FirebaseCredentialsFilePath);

        while (!cancelationToken.IsCancellationRequested)
        {
            try
            {
                await ProcessNotificationAsync(cancelationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing notification messages.");
            }

            await Task.Delay(_notificationProcessorOptions.IntervalInSeconds, cancelationToken);
        }
    }

    private async Task ProcessNotificationAsync(CancellationToken cancelationToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var messages =
            await unitOfWork.NotificationMessages.GetAllWithConditionAsync(x =>
                x.Status == NotificationMessageStatus.New, cancelationToken);

        var sender = scope.ServiceProvider.GetRequiredService<INotificationMessageSender>();
        var saver = scope.ServiceProvider.GetRequiredService<INotificationHistorySaver>();
        foreach (var message in messages)
        {
            var sendResult = await sender.SendAsync(message, cancelationToken);

            message.Status = sendResult ? NotificationMessageStatus.Success : NotificationMessageStatus.Failure;

            if (message.Status == NotificationMessageStatus.Success)
                await saver.SaveAsync(message, cancelationToken);
        }

        await unitOfWork.SaveChangesAsync(cancelationToken);
    }
}