using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class
    TokenStatusChangeProcessHandler(
        NotificationEntityMapperFactory mapperFactory,
        IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorTokenStatusChangeDto>, List<long>>
{
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorTokenStatusChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<TokenStatusChange, AggregatorTokenStatusChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<TokenStatusChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    private static async Task ProcessEntitiesAsync(
        List<TokenStatusChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.TokenStatusChange
                .GetListByIdsRawSqlAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.TokenStatusChange.AddAsync(entity, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task PreloadAndUnifyDetailsAsync(
        List<TokenStatusChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.TokenStatusChangeDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.TokenStatusChangeDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.TokenStatusChangeDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.TokenStatusChangeDetailsId;
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