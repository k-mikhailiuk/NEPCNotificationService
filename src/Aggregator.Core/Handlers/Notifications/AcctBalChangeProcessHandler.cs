using Aggregator.Core.Commands;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class AcctBalChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcctBalChangeDto>>
{
    private readonly IMediator _mediator;
    private readonly IAcctBalChangeRepository _acctBalChangeRepository;
    
    public AcctBalChangeProcessHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _acctBalChangeRepository = unitOfWork.AcctBalChange;
        _mediator = mediator;
    }
    public async Task Handle(ProcessNotificationCommand<AggregatorAcctBalChangeDto> request, CancellationToken cancellationToken)
    {
        foreach (var notification in request.Notifications)
        {
            await _acctBalChangeRepository.AddAsync(notification, cancellationToken);
        }
    }
}