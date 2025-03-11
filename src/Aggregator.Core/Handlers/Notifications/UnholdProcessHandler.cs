using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.Unhold;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class UnholdProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorUnholdDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public UnholdProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorUnholdDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<Unhold, AggregatorUnholdDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();
        
        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<Unhold>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList(); 
    }
    
    private static async Task ProcessEntitiesAsync(
        List<Unhold> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var existing =
                await unitOfWork.Unhold.ExistsAsync(x => x.NotificationId == entity.NotificationId,
                    cancellationToken);
            if (!existing)
                await unitOfWork.Unhold.AddAsync(entity, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }
    }

    private static async Task PreloadAndUnifyDetailsAsync(
        List<Unhold> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.UnholdDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.UnholdDetails
            .GetListAsync(d => allDetailsIds.Contains(d.UnholdDetailsId), cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.UnholdDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.UnholdDetailsId;
            if (detailsCache.TryGetValue(dId, out var existingDet))
            {
                entity.Details = existingDet;
            }
            else
            {
                detailsCache[dId] = entity.Details;
            }
        }
    }
}