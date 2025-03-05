using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DTOs.OwiUserAction;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class OwiUserActionProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorOwiUserActionDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public OwiUserActionProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorOwiUserActionDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<OwiUserAction, AggregatorOwiUserActionDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        throw new NotImplementedException();
    }
}