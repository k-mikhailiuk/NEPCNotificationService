using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DTOs.TokenStausChange;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class TokenStatusChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorTokenStatusChangeDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;

    public TokenStatusChangeProcessHandler(NotificationEntityMapperFactory mapperFactory)
    {
        _mapperFactory = mapperFactory;
    }

    public Task Handle(ProcessNotificationCommand<AggregatorTokenStatusChangeDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<TokenStatusChange, AggregatorTokenStatusChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        throw new NotImplementedException();
    }
}