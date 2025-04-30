using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

public class TokenStatusChangeLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<TokenStatusChange>
{
    public async Task<NotificationDataLoad<TokenStatusChange>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.TokenStatusChange
            .GetAll().Where(x => notificationIds.Contains(x.NotificationId))
            .Include(x => x.Details)
            .Include(x=>x.CardInfo).ToListAsync(cancellationToken);

        var messageText =
            await unitOfWork.NotificationMessageTextDirectories.FindAsync(x =>
                x.NotificationType == NotificationMessageType.TokenStatusChange, cancellationToken);

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
            await unitOfWork.Accounts.GetAccountCustomerMapAsync(
                messages.Select(x => accountNoParser.ParseAccountNo(x.Details.CardIdentifier.CardIdentifierValue!)).ToList(),
                cancellationToken);

        if (accountsMap.Count == 0)
            throw new ArgumentNullException($"{accountsMap} is null");

        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountsMap.GetValueOrDefault(rawCleanAccountsMap[m.Details.CardIdentifier.CardIdentifierValue]));

        var customerSettingsMap =
            await unitOfWork.PushNotificationSettings.GetUserSettingsIds(
                notificationToCustomer.Select(x => x.Value).ToList(), cancellationToken);

        return new NotificationDataLoad<TokenStatusChange>
        {
            Messages = messages,
            CustomerSettingsMap = customerSettingsMap,
            NotificationTextById = notificationTextById,
            NotificationToCustomer = notificationToCustomer
        };
    }
}