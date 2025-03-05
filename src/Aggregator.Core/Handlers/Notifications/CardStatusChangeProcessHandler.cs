using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DTOs.CardStatusChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class CardStatusChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorCardStatusChangeDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public CardStatusChangeProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorCardStatusChangeDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<CardStatusChange, AggregatorCardStatusChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        throw new NotImplementedException();
    }
}