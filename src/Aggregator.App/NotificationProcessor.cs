using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.App;

/// <summary>
/// Фоновый сервис для периодической обработки и отправки уведомлений.
/// Инициализирует Firebase и обрабатывает новые уведомления с заданным интервалом.
/// </summary>
public class NotificationProcessor(
    ILogger<NotificationProcessor> logger,
    IServiceProvider serviceProvider,
    IOptions<NotificationProcessorOptions> notificationProcessorOptions)
    : BackgroundService
{
    private readonly NotificationProcessorOptions _notificationProcessorOptions = notificationProcessorOptions.Value;

    /// <summary>
    /// Основной цикл фонового процесса.
    /// Инициализирует Firebase и запускает обработку уведомлений с задержкой между итерациями.
    /// </summary>
    /// <param name="cancelationToken">Токен отмены задачи.</param>
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

    /// <summary>
    /// Выполняет извлечение новых уведомлений, их отправку и сохранение истории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    private async Task ProcessNotificationAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var messages =
            await unitOfWork.NotificationMessage.GetAllWithConditionAsync(x =>
                x.Status == NotificationMessageStatus.New, cancellationToken);

        if (!_notificationProcessorOptions.IsNeedSendPush)
        {
            foreach (var message in messages)
                message.Status = NotificationMessageStatus.Declined;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return;
        }

        var sender = scope.ServiceProvider.GetRequiredService<INotificationMessageSender>();
        var saver = scope.ServiceProvider.GetRequiredService<INotificationHistorySaver>();
        foreach (var message in messages)
        {
            var sendResult = await sender.SendAsync(message, cancellationToken);

            message.Status = sendResult ? NotificationMessageStatus.Success : NotificationMessageStatus.Failure;

            if (message.Status == NotificationMessageStatus.Success)
                await saver.SaveAsync(message, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}