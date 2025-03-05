using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public IssFinAuthProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request, CancellationToken cancellationToken)
    {
        
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        


        throw new NotImplementedException();
    }
}