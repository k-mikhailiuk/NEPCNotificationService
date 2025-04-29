using Aggregator.Core.Models.DataLoads;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

public class AcsOtpDataLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<AcsOtp>
{
    public async Task<NotificationDataLoad<AcsOtp>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.AcsOtps
            .GetAll().Where(x => notificationIds.Contains(x.NotificationId))
            .Include(x => x.Details)
            .Include(x => x.CardInfo).ToListAsync(cancellationToken);

        var messageText =
            await unitOfWork.NotificationMessageTextDirectories.FindAsync(x =>
                x.NotificationType == NotificationMessageType.AcsOtp, cancellationToken);

        var notificationTextById = messages
            .ToDictionary(
                m => m.NotificationId,
                _ => messageText
            );

        var rawCleanAccountsMap = messages
            .ToDictionary(
                m => m.CardInfo.CardIdentifier.CardIdentifierValue!,
                m => accountNoParser.ParseAccountNo(m.CardInfo.CardIdentifier.CardIdentifierValue!)
            );
        
        var accountsMap =
            await unitOfWork.Accounts.GetAccountCustomerMapAsync(
                messages.Select(x => accountNoParser.ParseAccountNo(x.CardInfo.CardIdentifier.CardIdentifierValue!))
                    .ToList(),
                cancellationToken);

        if (accountsMap.Count == 0)
            throw new ArgumentNullException($"{accountsMap} is null");

        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountsMap.GetValueOrDefault(rawCleanAccountsMap[m.CardInfo.CardIdentifier.CardIdentifierValue!]));

        var customerSettingsMap =
            await unitOfWork.PushNotificationSettings.GetUserSettingsIds(
                notificationToCustomer.Select(x => x.Value).ToList(), cancellationToken);

        return new NotificationDataLoad<AcsOtp>
        {
            Messages = messages,
            CustomerSettingsMap = customerSettingsMap,
            NotificationTextById = notificationTextById,
            NotificationToCustomer = notificationToCustomer
        };
    }
}