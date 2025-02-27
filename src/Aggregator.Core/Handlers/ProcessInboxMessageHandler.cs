using Aggregator.Core.Commands;
using Common.Parsers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Handlers;

public class ProcessInboxMessageHandler : IRequestHandler<ProcessInboxMessageCommand>
{
    private readonly ILogger<ProcessInboxMessageHandler> _logger;

    public ProcessInboxMessageHandler(ILogger<ProcessInboxMessageHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProcessInboxMessageCommand request, CancellationToken cancellationToken)
    {
        foreach (var message in request.Messages)
        {
            var notification = InboxMessageParser.ParseInboxMessage(message.Payload);
            
            var type = notification?.GetType().Name;
            Console.WriteLine(notification);
        }
        
        return Task.CompletedTask;
    }
}