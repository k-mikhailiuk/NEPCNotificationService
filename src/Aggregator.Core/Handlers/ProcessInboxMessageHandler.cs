using Aggregator.Core.Commands;
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
        return Task.CompletedTask;
    }
}