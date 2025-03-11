using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class PinChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorPinChangeDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public PinChangeProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorPinChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<PinChange, AggregatorPinChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<PinChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    private static async Task ProcessEntitiesAsync(
        List<PinChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var existing =
                await unitOfWork.PinChange.ExistsAsync(x => x.NotificationId == entity.NotificationId,
                    cancellationToken);
            if (!existing)
                await unitOfWork.PinChange.AddAsync(entity, cancellationToken);

            await unitOfWork.SaveChangesAsync();
        }
    }

    private static async Task PreloadAndUnifyDetailsAsync(
        List<PinChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.PinChangeDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.PinChangeDetails
            .GetListAsync(d => allDetailsIds.Contains(d.PinChangeDetailsId), cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.PinChangeDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.PinChangeDetailsId;
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