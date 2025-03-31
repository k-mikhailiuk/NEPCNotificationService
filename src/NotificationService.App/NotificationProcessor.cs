using Aggregator.DataAccess.Entities.Enum;
using Microsoft.Extensions.Options;
using NotificationService.Core.Services.Abstractions;
using NotificationService.DataAccess;
using NotificationService.DataAccess.Abstractions;
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
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<NotificationServiceDbContext>();

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
            
            if(message.Status == NotificationMessageStatus.Success)
                await saver.SaveAsync(message, cancelationToken);
        }
        
        await unitOfWork.SaveChangesAsync(cancelationToken);
    }
}