using Aggregator.Core.Commands;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using Common;
using MediatR;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>>
{
    public Task Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request, CancellationToken cancellationToken)
    {
        
        var dtos = request.Notifications;

        var entities = new List<IssFinAuth>();

        foreach (var dto in dtos)
        {
            var tz = TimeZoneConverter.ConvertFromStringToUtc(dto.Time);
        }
        
        throw new NotImplementedException();
    }
}