using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcsOtp;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций AcsOtp.
/// </summary>
/// <remarks>
/// Класс реализует интерфейс INotificationMessageBuilder для формирования уведомлений по операциям AcsOtp.
/// </remarks>
public class AcsOtpNotificationMessageBuilder(
    IServiceProvider serviceProvider,
    INotificationCompositor<AcsOtp> notificationCompositor,
    INotificationDataLoader<AcsOtp> acsOtpLoader)
    : INotificationMessageBuilder<AcsOtp>
{
    /// <summary>
    /// Асинхронно формирует список уведомлений по заданным идентификаторам.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сформированных уведомлений.</returns>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, если unitOfWork или context равны null.
    /// </exception>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(IEnumerable<long> notificationIds,
        CancellationToken cancellationToken)
    {

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var loadedData = await acsOtpLoader.LoadDataForNotificationsAsync(notificationIds, unitOfWork, cancellationToken);
        
        return await notificationCompositor.ComposeAsync(loadedData.Messages, loadedData.NotificationTextById,
            loadedData.NotificationToCustomer, loadedData.CustomerSettingsMap, cancellationToken);
    }
}