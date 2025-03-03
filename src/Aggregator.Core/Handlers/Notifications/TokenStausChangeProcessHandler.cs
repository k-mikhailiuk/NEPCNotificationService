using Aggregator.Core.Commands;
using Aggregator.DTOs.TokenStausChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class TokenStausChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorTokenStausChangeDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorTokenStausChangeDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}