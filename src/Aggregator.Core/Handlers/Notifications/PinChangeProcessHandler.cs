using Aggregator.Core.Commands;
using Aggregator.DTOs.PinChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class PinChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorPinChangeDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorPinChangeDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}