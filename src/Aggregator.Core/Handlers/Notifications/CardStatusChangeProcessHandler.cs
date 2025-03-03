using Aggregator.Core.Commands;
using Aggregator.DTOs.CardStatusChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class CardStatusChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorCardStatusChangeDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorCardStatusChangeDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}