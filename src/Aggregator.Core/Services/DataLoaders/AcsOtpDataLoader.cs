using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using Common;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

/// <inheritdoc />
public class AcsOtpDataLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<AcsOtp>
{
    /// <inheritdoc />
    public async Task<NotificationDataLoad<AcsOtp>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork,
        IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await aggregatorUnitOfWork.AcsOtps
            .GetAll().Where(x => notificationIds.Contains(x.NotificationId))
            .Include(x => x.Details)
            .Include(x => x.CardInfo).ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            message.Details.OtpInfo.Otp = Encryptor.Decrypt(message.Details.OtpInfo.Otp);
        }

        var messageText =
            await controlPanelUnitOfWork.NotificationMessageTextDirectories.FindAsync(x =>
                x.NotificationType == NotificationMessageType.AcsOtp, cancellationToken);

        var notificationTextById = messages
            .ToDictionary(
                m => m.NotificationId,
                _ => messageText
            );

        var rawCleanAccountsMap = messages
            .Select(x=>x.CardInfo.CardIdentifier)
            .Distinct()
            .ToDictionary(
                m => m.CardIdentifierValue!,
                m => accountNoParser.ParseAccountNo(m.CardIdentifierValue!)
            );
        
        var accountsMap =
            await aggregatorUnitOfWork.Accounts.GetAccountCustomerMapAsync(
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
            await aggregatorUnitOfWork.PushNotificationSettings.GetUserSettingsIds(
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