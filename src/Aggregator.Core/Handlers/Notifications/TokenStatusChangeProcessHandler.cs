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
    TokenStatusChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorTokenStatusChangeDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public TokenStatusChangeProcessHandler(NotificationEntityMapperFactory mapperFactory,
        IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorTokenStatusChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<TokenStatusChange, AggregatorTokenStatusChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = _serviceProvider.CreateScope();
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
            var existing =
                await unitOfWork.TokenStatusChange.ExistsAsync(x => x.NotificationId == entity.NotificationId,
                    cancellationToken);
            if (!existing)
                await unitOfWork.TokenStatusChange.AddAsync(entity, cancellationToken);

            await unitOfWork.SaveChangesAsync();
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
            .GetListAsync(d => allDetailsIds.Contains(d.TokenStatusChangeDetailsId), cancellationToken);

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