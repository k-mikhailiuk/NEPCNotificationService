using Aggregator.Core.Commands;
using Aggregator.DTOs.OwiUserAction;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class OwiUserActionProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorOwiUserActionDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorOwiUserActionDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}