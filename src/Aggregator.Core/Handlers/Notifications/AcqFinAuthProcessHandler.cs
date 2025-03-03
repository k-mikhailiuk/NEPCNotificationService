using Aggregator.Core.Commands;
using Aggregator.DTOs.AcqFinAuth;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class AcqFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcqFinAuthDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorAcqFinAuthDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}