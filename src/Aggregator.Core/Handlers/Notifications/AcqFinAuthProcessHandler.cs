using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class AcqFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcqFinAuthDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public AcqFinAuthProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorAcqFinAuthDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = _mapperFactory.GetMapper<AcqFinAuth, AggregatorAcqFinAuthDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();
        
        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);
        
        await PreloadAndUnifyMerchantAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<AcqFinAuth>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork, cancellationToken);
        
        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }
    
    private static async Task ProcessEntitiesAsync(
        List<AcqFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var existing =
                await unitOfWork.AcqFinAuth.ExistsAsync(x => x.NotificationId == entity.NotificationId,
                    cancellationToken);
            if (!existing)
                await unitOfWork.AcqFinAuth.AddAsync(entity, cancellationToken);
            
            await unitOfWork.SaveChangesAsync();
        }
    }
    
    private static async Task PreloadAndUnifyDetailsAsync(
        List<AcqFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.AcqFinAuthDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.AcqFinAuthDetails
            .GetListAsync(d => allDetailsIds.Contains(d.AcqFinAuthDetailsId), cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.AcqFinAuthDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.AcqFinAuthDetailsId;
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
    
    private static async Task PreloadAndUnifyMerchantAsync(
        List<AcqFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allMerchantIds = entities
            .Select(e => e.MerchantInfo.Id)
            .Distinct()
            .ToList();

        var existingMerchants = await unitOfWork.MerchantInfo
            .GetListAsync(m => allMerchantIds.Contains(m.Id), cancellationToken);

        var merchantCache = existingMerchants.ToDictionary(m => m.Id, m => m);

        foreach (var entity in entities)
        {
            var mid = entity.MerchantInfo.Id;
            if (merchantCache.TryGetValue(mid, out var existingM))
            {
                entity.MerchantInfo = existingM;
            }
            else
            {
                merchantCache[mid] = entity.MerchantInfo;
            }
        }
    }
}