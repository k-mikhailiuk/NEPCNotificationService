using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using ControlPanel.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций IssFinAuth.
/// </summary>
/// <remarks>
/// Класс реализует интерфейс INotificationMessageBuilder для формирования уведомлений по операциям IssFinAuth.
/// </remarks>
public class IssFinAuthNotificationMessageBuilder(
    IServiceProvider serviceProvider,
    INotificationCompositor<IssFinAuth> notificationCompositor,
    INotificationDataLoader<IssFinAuth> issFinAuthLoader)
    : INotificationMessageBuilder<IssFinAuth>
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

        var loadedData =
            await issFinAuthLoader.LoadDataForNotificationsAsync(notificationIds, unitOfWork, controlPanelUnitOfWork,
                cancellationToken);

        loadedData.Messages = await AttachLimitsAsync(loadedData.Messages, unitOfWork, cancellationToken);

        return await notificationCompositor.ComposeAsync(loadedData.Messages, loadedData.NotificationTextById,
            loadedData.NotificationToCustomer, loadedData.CustomerSettingsMap, cancellationToken);
    }

    /// <summary>
    /// Загружает и группирует лимитные обёртки для карт и аккаунтов по идентификаторам уведомлений.
    /// </summary>
    /// <param name="messages">
    /// Коллекция сообщений <see cref="IssFinAuth"/>, у каждого из которых есть
    /// <see cref="IssFinAuth.NotificationId"/>, <see cref="IssFinAuth.CardInfoId"/> и список
    /// <see cref="IssFinAuth.AccountsInfo"/> с собственными <see cref="AccountInfo.Id"/>.
    /// </param>
    /// <param name="aggregatorUnitOfWork">
    /// Экземпляр <see cref="IAggregatorUnitOfWork"/>, предоставляющий репозитории
    /// <see cref="CardInfoLimitWrapper"/> и <see cref="AccountsInfoLimitWrapper"/>.
    /// </param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Кортеж, содержащий два словаря c обертками лимитов
    /// </returns>
    private static async Task<(Dictionary<long, List<CardInfoLimitWrapper>>,
            Dictionary<(long, long), List<AccountsInfoLimitWrapper>>)>
        CompositeLimits(IReadOnlyCollection<IssFinAuth> messages, IAggregatorUnitOfWork aggregatorUnitOfWork,
            CancellationToken cancellationToken)
    {
        var cardIdToNotificationMap = messages
            .ToDictionary(m => m.CardInfoId, m => m.NotificationId);

        var accInfoIdToNotificationMap = messages
            .SelectMany(m => m.AccountsInfo
                .Select(ai => new { ai.Id, m.NotificationId }))
            .ToDictionary(x => x.Id, x => x.NotificationId);

        var cardInfoLimitWrappersQuery = aggregatorUnitOfWork.CardInfoLimitWrapper
            .GetAll()
            .Where(x => cardIdToNotificationMap.Keys.Contains(x.CardInfoId))
            .Include(w => w.Limit);

        var accInfoLimitWrappersQuery = aggregatorUnitOfWork.AccountsInfoLimitWrapper
            .GetAll()
            .Where(x => accInfoIdToNotificationMap.Keys.Contains(x.AccountsInfoId))
            .Include(w => w.Limit);

        var cardWrappers = await cardInfoLimitWrappersQuery
            .ToListAsync(cancellationToken);

        var accWrappers = await accInfoLimitWrappersQuery
            .ToListAsync(cancellationToken);

        var cardMap = cardWrappers
            .GroupBy(w => cardIdToNotificationMap[w.CardInfoId])
            .ToDictionary(g => g.Key, g => g.ToList());

        var accCompositeMap = accWrappers
            .GroupBy(w => (
                NotificationId: accInfoIdToNotificationMap[w.AccountsInfoId],
                w.AccountsInfoId
            ))
            .ToDictionary(g => g.Key, g => g.ToList());

        return (cardMap, accCompositeMap);
    }

    /// <summary>
    /// Осуществялет привязку лимитов к сообщениям.
    /// </summary>
    /// <param name="messages">
    /// Коллекция сообщений <see cref="IssFinAuth"/>
    /// </param>
    /// <param name="aggregatorUnitOfWork">
    /// Экземпляр <see cref="IAggregatorUnitOfWork"/>
    /// </param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Сообщение с подгруженными сущностями лимитов
    /// </returns>
    private async Task<List<IssFinAuth>> AttachLimitsAsync(IEnumerable<IssFinAuth> messages,
        IAggregatorUnitOfWork aggregatorUnitOfWork,
        CancellationToken cancellationToken)
    {
        var messageList = messages.ToList();

        var messageIds = messageList
            .Select(m => m.NotificationId);

        var accountsInfo = await aggregatorUnitOfWork.AccountsInfos.GetAll()
            .Where(x => x.NotificationType == NotificationType.IssFinAuth && messageIds.Contains(x.NotificationId))
            .ToListAsync(cancellationToken);

        foreach (var message in messageList)
        {
            message.AccountsInfo = accountsInfo.Where(x => x.NotificationId == message.NotificationId).ToList();
        }

        var wrappers =
            await CompositeLimits((IReadOnlyCollection<IssFinAuth>)messages, aggregatorUnitOfWork, cancellationToken);

        foreach (var message in messageList)
        {
            message.CardInfo.Limits = wrappers.Item1[message.NotificationId];

            foreach (var accountInfo in message.AccountsInfo)
            {
                accountInfo.Limits = wrappers.Item2[(accountInfo.NotificationId, accountInfo.Id)];
            }
        }

        return messageList;
    }
}