using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.PinChange;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

/// <inheritdoc />
public class PinChangeDataLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<PinChange>
{
    /// <inheritdoc />
    public async Task<NotificationDataLoad<PinChange>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork,
        IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await aggregatorUnitOfWork.PinChange
            .GetAll().Where(x => notificationIds.Contains(x.NotificationId))
            .Include(x => x.Details)
            .Include(x => x.CardInfo).ToListAsync(cancellationToken);

        var messageText =
            await controlPanelUnitOfWork.NotificationMessageTextDirectories.FindAsync(x =>
                x.NotificationType == NotificationMessageType.PinChange, cancellationToken);

        var notificationTextById = messages
            .ToDictionary(
                m => m.NotificationId,
                _ => messageText
            );

        var rawCleanAccountsMap = messages
            .Select(x=>x.Details.CardIdentifier)
            .Distinct()
            .ToDictionary(
                m => m.CardIdentifierValue!,
                m => accountNoParser.ParseAccountNo(m.CardIdentifierValue!)
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

        return new NotificationDataLoad<PinChange>
        {
            Messages = messages,
            CustomerSettingsMap = customerSettingsMap,
            NotificationTextById = notificationTextById,
            NotificationToCustomer = notificationToCustomer
        };
    }
}