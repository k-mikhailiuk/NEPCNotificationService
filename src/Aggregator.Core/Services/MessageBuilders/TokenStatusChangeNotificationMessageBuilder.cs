using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций изменения статуса токена.
/// </summary>
/// <remarks>
/// Реализует интерфейс INotificationMessageBuilder для формирования уведомлений по операциям TokenStatusChange.
/// </remarks>
public class TokenStatusChangeNotificationMessageBuilder(
    IServiceProvider serviceProvider,
    INotificationCompositor<TokenStatusChange> notificationCompositor,
    INotificationDataLoader<TokenStatusChange> tokenStatusChangeLoader)
    : INotificationMessageBuilder<TokenStatusChange>
{

    /// <summary>
    /// Асинхронно формирует список уведомлений по заданным идентификаторам.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сформированных уведомлений.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если unitOfWork или context равны null.</exception>
    /// <exception cref="NullReferenceException">Выбрасывается, если текст уведомления не найден.</exception>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(IEnumerable<long> notificationIds,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var loadedData = await tokenStatusChangeLoader.LoadDataForNotificationsAsync(notificationIds, unitOfWork, cancellationToken);
        
        return await notificationCompositor.ComposeAsync(loadedData.Messages, loadedData.NotificationTextById,
            loadedData.NotificationToCustomer, loadedData.CustomerSettingsMap, cancellationToken);
    }
}