using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Common.Enums;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций IssFinAuth.
/// </summary>
/// <remarks>
/// Класс реализует интерфейс INotificationMessageBuilder для формирования уведомлений по операциям IssFinAuth.
/// </remarks>
public class IssFinAuthNotificationMessageBuilder(
    IOptions<NotificationMessageOptions> notificationMessageOptions,
    IServiceProvider serviceProvider,
    IKeyWordBuilder<IssFinAuth> keyWordBuilder,
    ICustomerIdSelector customerIdSelector)
    : INotificationMessageBuilder<IssFinAuth>
{
    private readonly NotificationMessageOptions _notificationMessageOptions = notificationMessageOptions.Value;

    /// <summary>
    /// Асинхронно формирует список уведомлений по заданным идентификаторам.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сформированных уведомлений.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если unitOfWork или context равны null.</exception>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

        var messages =
            await unitOfWork.IssFinAuth.GetByIdsWithIncludesAsync(notificationIds,
                cancellationToken, 
                x => x.Details,
                x=>x.CardInfo);
        
        var operationTypes = messages.Select(m => m.Details.TransType)
            .Distinct()
            .ToList();

        if (operationTypes.Count == 0)
            throw new ArgumentNullException($"{operationTypes} is null");
        
        var messageTextMap = await context.NotificationMessageTextDirectories
            .Where(x =>
                x.NotificationType == NotificationMessageType.AcctBalChange &&
                operationTypes.Contains((int)x.OperationType!))
            .ToDictionaryAsync(
                x => (int)x.OperationType!,
                x => x,
                cancellationToken
            );

        var accountIds = messages.Select(m => customerIdSelector.ParseAccountNo(m.Details.AccountId, m.Details.AccountId[..3])).ToList();
        
        var accounts = await unitOfWork.Accounts.GetByAccountNos(accountIds).ToListAsync(cancellationToken);
        
        var allAccountIds = messages.Select(m => m.Details.AccountId);
        var accountIdMap = await customerIdSelector
            .GetCustomerIdsAsync(allAccountIds, context, cancellationToken);
        
        var notificationToCustomer = messages
            .ToDictionary(
                m => m.NotificationId,
                m => accountIdMap.TryGetValue(m.Details.AccountId, out var cid));

        foreach (var message in messages)
        {
            var accountsInfo = await unitOfWork.AccountsInfos.GetAll(x =>
                x.NotificationId == message.NotificationId && x.NotificationType == NotificationType.IssFinAuth)
                .ToListAsync(cancellationToken);

            message.AccountsInfo = accountsInfo;
            

            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.IssFinAuth &&
                     (int)x.OperationType! == message.Details.TransType, cancellationToken);

            if (messageText == null)
                continue;

            if (!messageText.IsNeedSend)
                continue;

            var customerId = await customerIdSelector.GetCustomerIdAsync(message.Details.AccountId, context, cancellationToken);

            if (customerId == null)
                continue;

            var languageSelector = scope.ServiceProvider.GetRequiredService<ILanguageSelector>();

            var languageId = await languageSelector.GetLanguageIdAsync(customerId.Value, context, cancellationToken);

            var language = languageId.HasValue ? (Language)languageId.Value : Language.Russian;

            var localizeMessage = language switch
            {
                Language.Russian => messageText.MessageTextRu,
                Language.English => messageText.MessageTextEn,
                Language.Kyrgyz => messageText.MessageTextKg,
                _ => messageText.MessageTextRu
            };

            var notificationMessage = new NotificationMessage
            {
                Title = _notificationMessageOptions.Title,
                Status = NotificationMessageStatus.New,
                CustomerId = customerId.Value,
            };

            list.Add(notificationMessage);
        }

        return list;
    }

    /// <summary>
    /// Присоединяет лимиты для информации по карте и аккаунтам.
    /// </summary>
    /// <param name="message">Сообщение IssFinAuth, содержащее информацию по карте и аккаунтам.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к репозиториям.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Кортеж, содержащий список обёрток лимитов для информации по карте (ciWrapper)
    /// и обновлённый список информации по аккаунтам (accountsInfo).
    /// </returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если лимит не найден.</exception>
    private static async Task<(List<CardInfoLimitWrapper> ciWrapper, List<AccountsInfo> accountsInfo)>
        AttachLimitsAsync(IssFinAuth message, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        var cardInfoLimitWrappers =
            await unitOfWork.CardInfoLimitWrapper.GetAll(x => x.CardInfoId == message.CardInfoId)
                .ToListAsync(cancellationToken);

        foreach (var limitWrapper in cardInfoLimitWrappers)
        {
            var limit = await unitOfWork.Limit.GetByIdAsync(limitWrapper.LimitId, cancellationToken) ??
                        throw new InvalidOperationException();

            limitWrapper.Limit = limit;
        }

        var accInfoLimitWrappers = new List<AccountsInfoLimitWrapper>();

        foreach (var accountsInfo in message.AccountsInfo)
        {
            accInfoLimitWrappers = await unitOfWork.AccountsInfoLimitWrapper.GetAll(
                x => x.AccountsInfoId == accountsInfo.Id).ToListAsync(cancellationToken);

            accountsInfo.Limits = accInfoLimitWrappers;

            foreach (var limitWrapper in accountsInfo.Limits)
            {
                var limit = await unitOfWork.Limit.GetByIdAsync(limitWrapper.LimitId, cancellationToken) ??
                            throw new InvalidOperationException();

                limitWrapper.Limit = limit;
            }
        }
        
        return (cardInfoLimitWrappers, message.AccountsInfo);
    }
}