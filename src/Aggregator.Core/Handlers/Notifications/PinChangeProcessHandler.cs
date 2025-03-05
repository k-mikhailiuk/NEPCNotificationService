using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class PinChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorPinChangeDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public PinChangeProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorPinChangeDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<PinChange, AggregatorPinChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        throw new NotImplementedException();
    }
}