using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public IssFinAuthProcessHandler(
        NotificationEntityMapperFactory mapperFactory,
        IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();
        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyMerchantAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyLimitsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<IssFinAuth>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork, cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    private static async Task ProcessEntitiesAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            await ProcessDetailsLimitsAsync(entity.Details, unitOfWork, cancellationToken);
            
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.IssFinAuth
                .GetListByIdsRawSqlAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.IssFinAuth.AddAsync(entity, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task PreloadAndUnifyDetailsAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.IssFinAuthDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.IssFinAuthDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.IssFinAuthDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.IssFinAuthDetailsId;
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
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allMerchantIds = entities
            .Select(e => e.MerchantInfo.Id)
            .Distinct()
            .ToList();

        var existingMerchants = await unitOfWork.MerchantInfo
            .GetListByIdsRawSqlAsync(allMerchantIds, cancellationToken);

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

    private static async Task ProcessDetailsLimitsAsync(
        IssFinAuthDetails issFinAuthDetails,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (issFinAuthDetails.CheckedLimits == null)
            return;

        foreach (var limit in issFinAuthDetails.CheckedLimits)
        {
            var existingLimit = await unitOfWork.Limit.FindAsync(
                x => x.LimitId == limit.Id, cancellationToken);

            if (existingLimit == null)
            {
                await unitOfWork.CheckedLimit.AddAsync(limit, cancellationToken);
            }
        }
    }

    private static async Task PreloadAndUnifyLimitsAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var limitIds = new HashSet<long>();

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    limitIds.Add(lw.Limit.LimitId);
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null) continue;
                foreach (var lw in accInfo.Limits)
                {
                    limitIds.Add(lw.Limit.LimitId);
                }
            }
        }

        var existingLimits = await unitOfWork.Limit
            .GetListByIdsRawSqlAsync(limitIds.ToList(), cancellationToken);

        var limitCache = existingLimits.ToDictionary(l => l.LimitId, l => l);

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    var lid = lw.Limit.LimitId;
                    if (limitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        limitCache[lid] = lw.Limit;
                    }
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null)
                    continue;

                foreach (var lw in accInfo.Limits)
                {
                    var lid = lw.Limit.LimitId;
                    if (limitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        limitCache[lid] = lw.Limit;
                    }
                }
            }
        }
    }

    
}