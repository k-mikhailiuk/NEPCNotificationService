using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class AcctBalChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcctBalChangeDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    
    public AcctBalChangeProcessHandler(IUnitOfWork unitOfWork, NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }
    public async Task Handle(ProcessNotificationCommand<AggregatorAcctBalChangeDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<AcctBalChange, AggregatorAcctBalChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
    }
}