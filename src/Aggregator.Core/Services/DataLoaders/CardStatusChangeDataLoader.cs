using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

public class CardStatusChangeDataLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<CardStatusChange>
{
    public async Task<NotificationDataLoad<CardStatusChange>> LoadDataForNotificationsAsync(
        IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork,
        IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await aggregatorUnitOfWork.CardStatusChange
            .GetAll().Where(x => notificationIds.Contains(x.NotificationId))
            .Include(x => x.Details).ToListAsync(cancellationToken);

        var messageText =
            await controlPanelUnitOfWork.NotificationMessageTextDirectories.FindAsync(x =>
                x.NotificationType == NotificationMessageType.CardStatusChange, cancellationToken);

        var notificationTextById = messages
            .ToDictionary(
                m => m.NotificationId,
                _ => messageText
            );

        var rawCleanAccountsMap = messages
            .ToDictionary(
                m => m.Details.CardIdentifier.CardIdentifierValue,
                m => accountNoParser.ParseAccountNo(m.Details.CardIdentifier.CardIdentifierValue!)
            );

        var accountsMap =
            await aggregatorUnitOfWork.Accounts.GetAccountCustomerMapAsync(
                messages.Select(x => accountNoParser.ParseAccountNo(x.Details.CardIdentifier.CardIdentifierValue!))
                    .ToList(),
                cancellationToken);

        if (accountsMap.Count == 0)
            throw new ArgumentNullException($"{accountsMap} is null");

        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountsMap.GetValueOrDefault(rawCleanAccountsMap[m.Details.CardIdentifier.CardIdentifierValue]));

        var customerSettingsMap =
            await aggregatorUnitOfWork.PushNotificationSettings.GetUserSettingsIds(
                notificationToCustomer.Select(x => x.Value).ToList(), cancellationToken);

        return new NotificationDataLoad<CardStatusChange>
        {
            Messages = messages,
            CustomerSettingsMap = customerSettingsMap,
            NotificationTextById = notificationTextById,
            NotificationToCustomer = notificationToCustomer
        };
    }
}