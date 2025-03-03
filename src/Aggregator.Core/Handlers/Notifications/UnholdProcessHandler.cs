using Aggregator.Core.Commands;
using Aggregator.DTOs.Unhold;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class UnholdProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorUnholdDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorUnholdDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}