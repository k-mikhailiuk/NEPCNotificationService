using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class CardStatusChangeProcessHandler(
    NotificationEntityMapperFactory mapperFactory,
    IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorCardStatusChangeDto>, List<long>>
{
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorCardStatusChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<CardStatusChange, AggregatorCardStatusChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);
        
        await UnifyProcessorExtension<CardStatusChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);
        
        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    private static async Task ProcessEntitiesAsync(
        List<CardStatusChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.CardStatusChange
                .GetListByIdsRawSqlAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.CardStatusChange.AddAsync(entity, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task PreloadAndUnifyDetailsAsync(
        List<CardStatusChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.CardStatusChangeDetailsId)
            .Distinct()
            .ToList();
        
        var existingDetailsList = await unitOfWork.CardStatusChangeDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.CardStatusChangeDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.CardStatusChangeDetailsId;
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