using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.IssFinAuth;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

public class IssFinAuthDataLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<IssFinAuth>
{
    public async Task<NotificationDataLoad<IssFinAuth>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork,
        IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken)
    {
        try
        {
            var messages = await aggregatorUnitOfWork.IssFinAuth
                .GetAll().Where(x => notificationIds.Contains(x.NotificationId))
                .Include(x => x.Details)
                .Include(x => x.CardInfo)
                .Include(x=>x.MerchantInfo).ToListAsync(cancellationToken);

            var operationTypes = messages.Select(m => m.Details.TransType)
                .Distinct()
                .ToList();

            if (operationTypes.Count == 0)
                throw new ArgumentNullException($"{operationTypes} is null");

            var messageTextMap = await controlPanelUnitOfWork.NotificationMessageTextDirectories.GetAll()
                .Where(x =>
                    x.NotificationType == NotificationMessageType.IssFinAuth &&
                    operationTypes.Contains((int)x.OperationType!))
                .ToDictionaryAsync(
                    x => (int)x.OperationType!,
                    x => x,
                    cancellationToken
                );

            var notificationTextById = messages
                .Where(m => messageTextMap.ContainsKey(m.Details.TransType))
                .ToDictionary(
                    m => m.NotificationId,
                    m => messageTextMap[m.Details.TransType]
                );

            var rawCleanAccountsMap = messages
                .ToDictionary(
                    m => m.Details.AccountId,
                    m => accountNoParser.ParseAccountNo(m.Details.AccountId)
                );

            var accountsMap =
                await aggregatorUnitOfWork.Accounts.GetAccountCustomerMapAsync(rawCleanAccountsMap.Select(x => x.Value).ToList(),
                    cancellationToken);

            if (accountsMap.Count == 0)
                throw new ArgumentNullException($"{accountsMap} is null");

            var notificationToCustomer = messages
                .ToDictionary(
                    m => m.NotificationId,
                    m => accountsMap.GetValueOrDefault(rawCleanAccountsMap[m.Details.AccountId]));

            var customerSettingsMap =
                await aggregatorUnitOfWork.PushNotificationSettings.GetUserSettingsIds(
                    notificationToCustomer.Select(x => x.Value).ToList(), cancellationToken);

            return new NotificationDataLoad<IssFinAuth>
            {
                Messages = messages,
                CustomerSettingsMap = customerSettingsMap,
                NotificationTextById = notificationTextById,
                NotificationToCustomer = notificationToCustomer
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}