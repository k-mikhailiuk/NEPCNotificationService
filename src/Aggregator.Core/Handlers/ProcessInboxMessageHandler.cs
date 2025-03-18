using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;
using Common;
using Common.Parsers;
using DataIngrestorApi.DataAccess.Entities;
using DataIngrestorApi.DataAccess.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Handlers;

public class ProcessInboxMessageHandler : IRequestHandler<ProcessInboxMessageCommand>
{
    private readonly ILogger<ProcessInboxMessageHandler> _logger;
    private readonly IMediator _mediator;
    private readonly INotificationCommandFactory _commandFactory;
    private readonly IServiceProvider _serviceProvider;
    private readonly INotificationMessageBuilderFactory _notificationMessageBuilderFactory;

    public ProcessInboxMessageHandler(ILogger<ProcessInboxMessageHandler> logger, IMediator mediator,
        INotificationCommandFactory commandFactory, IServiceProvider serviceProvider,
        INotificationMessageBuilderFactory notificationMessageBuilderFactory)
    {
        _logger = logger;
        _mediator = mediator;
        _commandFactory = commandFactory;
        _serviceProvider = serviceProvider;
        _notificationMessageBuilderFactory = notificationMessageBuilderFactory;
    }

    public async Task Handle(ProcessInboxMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing inbox messages: {MessageCount}", request.Messages.Count());

        var notificationsByType = new Dictionary<Type, List<object>>();

        var processedMessages = new Dictionary<long, long>();

        foreach (var message in request.Messages)
        {
            var notification = InboxMessageParser.ParseInboxMessage(message.Payload);

            if (notification == null)
            {
                _logger.LogWarning("Не удалось распарсить уведомление. Id={messageId}", message.Id);
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

            _logger.LogInformation("Parsed notification: Id={messageId}, Type={notificationType}", message.Id,
                notificationType.Name);
        }

        await UpdateStatusAsync(processedMessages.Keys.ToList(), InboxMessageStatus.InProgress, cancellationToken);

        _logger.LogInformation("Set in progress status messages: {MessagesCount}", processedMessages.Count);

        foreach (var (type, notifications) in notificationsByType)
        {
            try
            {
                var command = _commandFactory.CreateCommand(notifications);
                var processedNotificationsIds = await _mediator.Send(command, cancellationToken);

                var inboxMessageIds = processedNotificationsIds.Select(processedNotificationId =>
                    processedMessages.FirstOrDefault(x => x.Value == processedNotificationId).Key).ToList();

                await CompleteMessageProcessingAsync(inboxMessageIds, cancellationToken);
                
                NotificationTypeMapper.AggregatorTypeEntityTypeMapping.TryGetValue(type,
                    out var notificationEntityType);

                if (notificationEntityType == null)
                    throw new InvalidOperationException($"Unknown aggregatorNotification type: {type}");

                await CompositeAndSaveNotificationMessageAsync(notificationEntityType, processedNotificationsIds,
                    cancellationToken);
            }
            catch (NotSupportedException ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
    }

    private async Task UpdateStatusAsync(List<long> messageIds, InboxMessageStatus newStatus,
        CancellationToken cancellationToken)
    {
        if (messageIds.Count == 0)
            return;

        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var inClause = string.Join(",", messageIds);

        var messages = await unitOfWork
            .FromSql<InboxMessage>($"SELECT * FROM [nepc].[InboxMessages] WHERE Id IN ({inClause})")
            .ToListAsync(cancellationToken);

        foreach (var msg in messages)
        {
            msg.Status = newStatus;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated {count} messages to status={status}", messages.Count, newStatus);
    }

    private async Task CompleteMessageProcessingAsync(List<long> messageIds, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get {count} processed messages", messageIds.Count);

        await UpdateStatusAsync(messageIds, InboxMessageStatus.Completed, cancellationToken);

        using var scope = _serviceProvider.CreateScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var inboxMessages = await unitOfWork.Inbox.GetListByIdsRawSqlAsync(messageIds, cancellationToken);

        var inboxArchive = inboxMessages.Select(inboxMessage => InboxArchiveMessage.Create(inboxMessage.Payload))
            .ToList();

        await unitOfWork.InboxArchiveMessage.AddRangeAsync(inboxArchive, cancellationToken);

        _logger.LogInformation("{count} messages successfully added to archive", inboxArchive.Count);

        unitOfWork.Inbox.RemoveRange(inboxMessages);

        _logger.LogInformation("{count} messages successfully removed from inbox", inboxMessages.Count);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task CompositeAndSaveNotificationMessageAsync(Type entityNotificationsType,
        List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var builder = _notificationMessageBuilderFactory.CreateNotificationMessageBuilder(entityNotificationsType);
        
        var messages=await builder.BuildNotificationAsync(notificationIds, cancellationToken);
        
        using var scope = _serviceProvider.CreateScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await unitOfWork.NotificationMessage.AddRangeAsync(messages, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}