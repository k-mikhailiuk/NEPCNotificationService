using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.Unhold;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class UnholdProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorUnholdDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public UnholdProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorUnholdDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<Unhold, AggregatorUnholdDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        throw new NotImplementedException();
    }
}