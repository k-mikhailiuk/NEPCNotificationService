using Microsoft.Extensions.Options;
using NotificationService.Core.Services.Abstractions;
using NotificationService.DataAccess;
using OptionsConfiguration;

namespace NotificationService.App;

public class NotificationProcessor : BackgroundService
{
    private readonly ILogger<NotificationProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly NotificationProcessorOptions _notificationProcessorOptions;

    public NotificationProcessor(ILogger<NotificationProcessor> logger, IServiceProvider serviceProvider,
        IOptions<NotificationProcessorOptions> notificationProcessorOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _notificationProcessorOptions = notificationProcessorOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancelationToken)
    {
        _logger.LogInformation("Notification processor started");
        
        FirebaseInitializer.Init(_notificationProcessorOptions.FirebaseCredentialsFilePath);

        while (!cancelationToken.IsCancellationRequested)
        {
            try
            {
                await ProcessNotificationAsync(cancelationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing notification messages.");
            }

            await Task.Delay(_notificationProcessorOptions.IntervalInSeconds, cancelationToken);
        }
    }

    private async Task ProcessNotificationAsync(CancellationToken cancelationToken)
    {
        var context = _serviceProvider.GetRequiredService<NotificationServiceDbContext>();
        
        
        
        //var messages = await notificationReceiver.GetNotificationsAsync();

        //var buildedNotifications = await notificationBuilder.BuildNotificationAsync(messages);

        //await notificationSender.SendNotificationAsync(buildedNotifications);
    }
}