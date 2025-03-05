using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DTOs.AcqFinAuth;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class AcqFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcqFinAuthDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public AcqFinAuthProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorAcqFinAuthDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<AcqFinAuth, AggregatorAcqFinAuthDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        throw new NotImplementedException();
    }
}