using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DTOs.Abstractions;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DataAccess.Entities.Enums;
using Mapper;
using Mapper.Parsers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Handlers;

/// <summary>
/// Обработчик входящих сообщений.
/// </summary>
public class InboxHandler(
    ILogger<InboxHandler> logger,
    IMediator mediator,
    INotificationCommandFactory commandFactory,
    IServiceProvider serviceProvider,
    INotificationMessageBuilderFactory notificationMessageBuilderFactory)
    : IInboxHandler
{
    /// <summary>
    /// Обрабатывает входящие сообщения, обновляет их статус, создаёт команды уведомлений и архивирует сообщения.
    /// </summary>
    /// <param name="request">Список входящих сообщений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task HandleAsync(IEnumerable<InboxMessage> request, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        unitOfWork.BeginTransactionAsync();

        var inboxMessages = request.ToList();
        logger.LogInformation("Processing inbox messages: {MessageCount}", inboxMessages.Count);

        var notificationsByType = new Dictionary<Type, List<INotificationAggregatorDto>>();

        var processedMessages = new Dictionary<long, long>();

        foreach (var message in inboxMessages)
        {
            var notification = InboxMessageParser.ParseInboxMessage(message.Payload);

            if (notification == null)
            {
                logger.LogWarning("Не удалось распарсить уведомление. Id={messageId}", message.Id);
                continue;
            }

            processedMessages.Add(message.Id, notification.Id);

            var notificationType = notification.GetType();

            if (notificationsByType.TryGetValue(notificationType, out var notificationList))
                notificationList.Add(notification);
            else
                notificationsByType.Add(notificationType, [notification]);

            notificationList?.Add(notification);

            logger.LogInformation("Parsed notification: Id={messageId}, Type={notificationType}", message.Id,
                notificationType.Name);
        }

        await UpdateStatusAsync(processedMessages.Keys.ToList(), InboxMessageStatus.InProgress, unitOfWork,
            cancellationToken);

        logger.LogInformation("Set in progress status messages: {MessagesCount}", processedMessages.Count);

        foreach (var (type, notifications) in notificationsByType)
        {
            try
            {
                var command = commandFactory.CreateCommand(notifications);
                var processedNotificationsIds = await mediator.Send(command, cancellationToken);

                var reverseMap = processedMessages.ToDictionary(x => x.Value, x => x.Key);

                var inboxMessageIds = new List<long>();
                foreach (var notificationsId in processedNotificationsIds)
                    if (reverseMap.TryGetValue(notificationsId, out var messageId))
                        inboxMessageIds.Add(messageId);
                
                await CompleteMessageProcessingAsync(inboxMessageIds, unitOfWork, cancellationToken);

                NotificationTypeMapper.AggregatorTypeEntityTypeMapping.TryGetValue(type,
                    out var notificationEntityType);

                if (notificationEntityType == null)
                    throw new InvalidOperationException($"Unknown aggregatorNotification type: {type}");

                await CompositeAndSaveNotificationMessageAsync(notificationEntityType, processedNotificationsIds,
                    unitOfWork,
                    cancellationToken);
                
                unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                unitOfWork.RollbackTransactionAsync();
            }
        }
    }

    /// <summary>
    /// Обновляет статус сообщений по их идентификаторам.
    /// </summary>
    /// <param name="messageIds">Список идентификаторов сообщений.</param>
    /// <param name="newStatus">Новый статус сообщения.</param>
    /// <param name="unitOfWork">Интерфейс работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private async Task UpdateStatusAsync(List<long> messageIds, InboxMessageStatus newStatus, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (messageIds.Count == 0)
            return;

        var messages = await unitOfWork.Inbox.GetQueryByIds(messageIds).ToListAsync(cancellationToken);

        foreach (var msg in messages)
            msg.Status = newStatus;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated {count} messages to status={status}", messages.Count, newStatus);
    }

    /// <summary>
    /// Завершает обработку сообщений: обновляет статус, архивирует и удаляет сообщения.
    /// </summary>
    /// <param name="messageIds">Список идентификаторов сообщений.</param>
    /// <param name="unitOfWork">Интерфейс работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private async Task CompleteMessageProcessingAsync(List<long> messageIds, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get {count} processed messages", messageIds.Count);

        await UpdateStatusAsync(messageIds, InboxMessageStatus.Completed, unitOfWork, cancellationToken);

        var inboxMessages = unitOfWork.Inbox.GetQueryByIds(messageIds);

        var inboxArchive = inboxMessages.Select(inboxMessage => InboxArchiveMessage.Create(inboxMessage.Payload));

        unitOfWork.InboxArchiveMessage.AddRange(inboxArchive);

        logger.LogInformation("{count} messages successfully added to archive", messageIds.Count);

        unitOfWork.Inbox.RemoveRange(inboxMessages);

        logger.LogInformation("{count} messages successfully removed from inbox", messageIds.Count);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Композитирует и сохраняет сообщение уведомления.
    /// </summary>
    /// <param name="entityNotificationsType">Тип сущности уведомления.</param>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="unitOfWork">Интерфейс работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private async Task CompositeAndSaveNotificationMessageAsync(Type entityNotificationsType,
        List<long> notificationIds,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var builder = notificationMessageBuilderFactory.CreateNotificationMessageBuilder(entityNotificationsType);

        var messages = await builder.BuildNotificationAsync(notificationIds, cancellationToken);

        unitOfWork.NotificationMessage.AddRange(messages);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}