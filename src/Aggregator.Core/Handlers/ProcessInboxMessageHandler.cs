using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories;
using Common.Parsers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Handlers;

public class ProcessInboxMessageHandler : IRequestHandler<ProcessInboxMessageCommand>
{
    private readonly ILogger<ProcessInboxMessageHandler> _logger;
    private readonly IMediator _mediator;
    private readonly INotificationCommandFactory _commandFactory;

    public ProcessInboxMessageHandler(ILogger<ProcessInboxMessageHandler> logger, IMediator mediator, INotificationCommandFactory commandfactory)
    {
        _logger = logger;
        _mediator = mediator;
        _commandFactory = commandfactory;
    }

    public async Task Handle(ProcessInboxMessageCommand request, CancellationToken cancellationToken)
    {
        var notificationsByType = new Dictionary<Type, List<object>>();
        
        foreach (var message in request.Messages)
        {
            var notification = InboxMessageParser.ParseInboxMessage(message.Payload);
            
            if (notification == null)
            {
                _logger.LogWarning("Не удалось распарсить уведомление.");
                continue;
            }
            
            var notificationType = notification.GetType();
            
            if (!notificationsByType.TryGetValue(notificationType, out var notificationList))
            {
                notificationList = [];
                notificationsByType[notificationType] = notificationList;
            }

            notificationList.Add(notification);
            
            _logger.LogInformation("Notification type {notification}", notification);
        }
        
        foreach (var (type, notifications) in notificationsByType)
        {
            try
            {
                var command = _commandFactory.CreateCommand(notifications);
                await _mediator.Send(command, cancellationToken);
            }
            catch (NotSupportedException ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
    }
}