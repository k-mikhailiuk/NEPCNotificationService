using Aggregator.Core.Models;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Services.DataLoaders;

/// <inheritdoc />
public class AcqFinAuthDataLoader : INotificationDataLoader<AcqFinAuth>
{
    /// <inheritdoc />
    public async Task<NotificationDataLoad<AcqFinAuth>> LoadDataForNotificationsAsync(IEnumerable<long> notificationIds,
        IAggregatorUnitOfWork aggregatorUnitOfWork,
        IControlPanelUnitOfWork controlPanelUnitOfWork,
        CancellationToken cancellationToken)
    {
        var messages = await aggregatorUnitOfWork.AcqFinAuth
            .GetAll().Where(x=> notificationIds.Contains(x.NotificationId))
            .Include(x=>x.Details)
            .Include(x=>x.MerchantInfo).ToListAsync(cancellationToken);

        var operationTypes = messages.Select(m => m.Details.TransType)
            .Distinct()
            .ToList();

        if (operationTypes.Count == 0)
            throw new ArgumentNullException($"{operationTypes} is null");

        var messageTextMap = await controlPanelUnitOfWork.NotificationMessageTextDirectories.GetAll()
            .Where(x =>
                x.NotificationType == NotificationMessageType.AcqFinAuth &&
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
                m => m.MerchantInfo,
                m => m.MerchantInfo.TerminalId
            );

        var accountsMap =
            await aggregatorUnitOfWork.Offices.GetCustomerIdsByTerminalsAsync(rawCleanAccountsMap.Select(x => x.Value).ToList()!,
                cancellationToken);

        if (accountsMap.Count == 0)
            throw new ArgumentNullException($"{accountsMap} is null");

        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountsMap.GetValueOrDefault(m.MerchantInfo.TerminalId));

        var customerSettingsMap =
            await aggregatorUnitOfWork.PushNotificationSettings.GetUserSettingsIds(
                notificationToCustomer.Select(x => x.Value).ToList(), cancellationToken);

        return new NotificationDataLoad<AcqFinAuth>
        {
            Messages = messages,
            CustomerSettingsMap = customerSettingsMap,
            NotificationTextById = notificationTextById,
            NotificationToCustomer = notificationToCustomer
        };
    }
}