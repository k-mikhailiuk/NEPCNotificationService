using Aggregator.Core.Commands;
using Aggregator.DTOs.IssFinAuth;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}