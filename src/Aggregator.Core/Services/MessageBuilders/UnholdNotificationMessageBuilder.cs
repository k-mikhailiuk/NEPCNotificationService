using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Unhold;
using ControlPanel.DataAccess.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций "Unhold".
/// </summary>
/// <remarks>
/// Реализует интерфейс INotificationMessageBuilder для формирования уведомлений по операциям Unhold.
/// </remarks>
public class UnholdNotificationMessageBuilder(
    IServiceProvider serviceProvider,
    INotificationCompositor<Unhold> notificationCompositor,
    INotificationDataLoader<Unhold> unholdLoader)
    : INotificationMessageBuilder<Unhold>
{
    /// <summary>
    /// Асинхронно формирует список уведомлений по заданным идентификаторам.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сформированных уведомлений.</returns>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(IEnumerable<long> notificationIds,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IAggregatorUnitOfWork>();
        using var controlPanelUnitOfWork = scope.ServiceProvider.GetRequiredService<IControlPanelUnitOfWork>();
        
        var loadedData = await unholdLoader.LoadDataForNotificationsAsync(notificationIds, unitOfWork, controlPanelUnitOfWork, cancellationToken);
        
        return await notificationCompositor.ComposeAsync(loadedData.Messages, loadedData.NotificationTextById,
            loadedData.NotificationToCustomer, loadedData.CustomerSettingsMap, cancellationToken);
    }
}