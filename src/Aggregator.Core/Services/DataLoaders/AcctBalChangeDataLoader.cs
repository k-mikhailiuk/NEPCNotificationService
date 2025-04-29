using Aggregator.Core.Models.DataLoads;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcctBalChange;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

public class AcctBalChangeDataLoader(IAccountNoParser accountNoParser) : INotificationDataLoader<AcctBalChange>
{
    public async Task<NotificationDataLoad<AcctBalChange>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.AcctBalChange
            .GetAll().Where(x=> notificationIds.Contains(x.NotificationId))
            .Include(x=>x.Details)
            .Include(x=>x.CardInfo).ToListAsync(cancellationToken);

        var operationTypes = messages.Select(m => m.Details.TransType)
            .Distinct()
            .ToList();

        if (operationTypes.Count == 0)
            throw new ArgumentNullException($"{operationTypes} is null");

        var messageTextMap = await unitOfWork.NotificationMessageTextDirectories.GetAll()
            .Where(x =>
                x.NotificationType == NotificationMessageType.AcctBalChange &&
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
            await unitOfWork.Accounts.GetAccountCustomerMapAsync(rawCleanAccountsMap.Select(x => x.Value).ToList(),
                cancellationToken);

        if (accountsMap.Count == 0)
            throw new ArgumentNullException($"{accountsMap} is null");

        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountsMap.GetValueOrDefault(rawCleanAccountsMap[m.Details.AccountId]));

        var customerSettingsMap =
            await unitOfWork.PushNotificationSettings.GetUserSettingsIds(
                notificationToCustomer.Select(x => x.Value).ToList(), cancellationToken);

        return new NotificationDataLoad<AcctBalChange>
        {
            Messages = messages,
            CustomerSettingsMap = customerSettingsMap,
            NotificationTextById = notificationTextById,
            NotificationToCustomer = notificationToCustomer
        };
    }
}