using Aggregator.Core.Commands;
using Aggregator.DTOs.TokenStausChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class TokenStatusChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorTokenStatusChangeDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorTokenStatusChangeDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}