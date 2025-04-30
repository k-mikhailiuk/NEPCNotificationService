using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using ControlPanel.DataAccess.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель сообщений уведомлений для AcctBalChange.
/// Формирует объект <see cref="NotificationMessage"/> на основе уведомления AcctBalChange, используя локализованный шаблон сообщения.
/// </summary>
public class AcctBalChangeNotificationMessageBuilder(
    IServiceProvider serviceProvider,
    INotificationCompositor<AcctBalChange> notificationCompositor,
    INotificationDataLoader<AcctBalChange> acctBalChangeLoader)
    : INotificationMessageBuilder<AcctBalChange>
{
    /// <summary>
    /// Асинхронно строит список сообщений уведомлений для заданных идентификаторов уведомлений AcctBalChange.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений AcctBalChange.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объектов <see cref="NotificationMessage"/>.</returns>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(IEnumerable<long> notificationIds,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IAggregatorUnitOfWork>();
        using var controlPanelUnitOfWork = scope.ServiceProvider.GetRequiredService<IControlPanelUnitOfWork>();
        
        var loadedData =
            await acctBalChangeLoader.LoadDataForNotificationsAsync(notificationIds, unitOfWork, controlPanelUnitOfWork, cancellationToken);
        
        return await notificationCompositor.ComposeAsync(loadedData.Messages, loadedData.NotificationTextById,
            loadedData.NotificationToCustomer, loadedData.CustomerSettingsMap, cancellationToken);
    }
}