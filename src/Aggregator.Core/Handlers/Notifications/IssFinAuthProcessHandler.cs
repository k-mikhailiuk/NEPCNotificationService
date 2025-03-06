using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public IssFinAuthProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));
        
        await ProcessEntities(entities, cancellationToken);

        throw new NotImplementedException();
    }

    private async Task ProcessEntities(IEnumerable<IssFinAuth> entities, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        foreach (var entity in entities)
        {
            var cardInfoEntity = entity.CardInfo;

            foreach (var cardInfoLimitWrapper in entity.CardInfo.Limits)
            {
               var limit = cardInfoLimitWrapper.Limit;
               await unitOfWork.Limit.AddAsync(limit, cancellationToken);
               await unitOfWork.CardInfoLimitWrapper.AddAsync(cardInfoLimitWrapper, cancellationToken);
            }
            await unitOfWork.CardInfo.AddAsync(cardInfoEntity, cancellationToken);

            await unitOfWork.SaveChangesAsync();
            
            
            var merchantInfoEntity = entity.MerchantInfo;
            
            
            
            var issFinAuthDetailsEntity = entity.Details;
            var ext = entity.Extensions;
            
        }
    }
}