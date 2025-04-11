using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;
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
/// Обработчик команды для обработки входящих сообщений.
/// </summary>
public class ProcessInboxMessageHandler(
    ILogger<ProcessInboxMessageHandler> logger,
    IMediator mediator,
    INotificationCommandFactory commandFactory,
    IServiceProvider serviceProvider,
    INotificationMessageBuilderFactory notificationMessageBuilderFactory)
    : IRequestHandler<ProcessInboxMessageCommand>
{
    /// <summary>
    /// Обрабатывает входящие сообщения, обновляет их статус, создаёт команды уведомлений и архивирует сообщения.
    /// </summary>
    /// <param name="request">Команда с входящими сообщениями.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(ProcessInboxMessageCommand request, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        unitOfWork.BeginTransactionAsync();
        
        logger.LogInformation("Processing inbox messages: {MessageCount}", request.Messages.Count());

        var notificationsByType = new Dictionary<Type, List<object>>();

        var processedMessages = new Dictionary<long, long>();

        foreach (var message in request.Messages)
        {
            var notification = InboxMessageParser.ParseInboxMessage(message.Payload);

            if (notification == null)
            {
                logger.LogWarning("Не удалось распарсить уведомление. Id={messageId}", message.Id);
                continue;
            }

            processedMessages.Add(message.Id, notification.Id);

            var notificationType = notification.GetType();

            if (!notificationsByType.TryGetValue(notificationType, out var notificationList))
            {
                notificationList = [];
                notificationsByType[notificationType] = notificationList;
            }

            notificationList.Add(notification);

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

                var inboxMessageIds = processedNotificationsIds.Select(processedNotificationId =>
                    processedMessages.FirstOrDefault(x => x.Value == processedNotificationId).Key).ToList();

                if(inboxMessageIds.Count == 0)
                    return;
                
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

        var inClause = string.Join(",", messageIds);

        var messages = await unitOfWork
            .FromSql<InboxMessage>($"SELECT * FROM [nepc].[InboxMessages] WHERE Id IN ({inClause})")
            .ToListAsync(cancellationToken);

        foreach (var msg in messages)
        {
            msg.Status = newStatus;
        }

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

        var inboxMessages = await unitOfWork.Inbox.GetListByIdsRawSqlAsync(messageIds, cancellationToken);

        var inboxArchive = inboxMessages.Select(inboxMessage => InboxArchiveMessage.Create(inboxMessage.Payload))
            .ToList();

        await unitOfWork.InboxArchiveMessage.AddRangeAsync(inboxArchive, cancellationToken);

        logger.LogInformation("{count} messages successfully added to archive", inboxArchive.Count);

        unitOfWork.Inbox.RemoveRange(inboxMessages);

        logger.LogInformation("{count} messages successfully removed from inbox", inboxMessages.Count);

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

        await unitOfWork.NotificationMessage.AddRangeAsync(messages, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}