using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler(
    NotificationEntityMapperFactory mapperFactory,
    IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>, List<long>>
{
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();
        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyMerchantAsync(entities, unitOfWork, cancellationToken);
        
        await PreloadAndUnifyLimitWrappersAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyLimitsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<IssFinAuth>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);

        return entities.Select(x => x.NotificationId).ToList();
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
        if (issFinAuthDetails.CheckedLimits == null || !issFinAuthDetails.CheckedLimits.Any())
            return;

        var allLimitIds = issFinAuthDetails.CheckedLimits
            .Select(l => l.Id)
            .Distinct()
            .ToList();

        var existingLimits = await unitOfWork.CheckedLimit
            .GetListByIdsRawSqlAsync(allLimitIds, cancellationToken);
        var limitCache = existingLimits.ToDictionary(l => l.Id, l => l);

        for (var i = 0; i < issFinAuthDetails.CheckedLimits.Count; i++)
        {
            var limit = issFinAuthDetails.CheckedLimits[i];
            if (limitCache.TryGetValue(limit.Id, out var existingLimit))
            {
                issFinAuthDetails.CheckedLimits[i] = existingLimit;
            }
            else
            {
                await unitOfWork.CheckedLimit.AddAsync(limit, cancellationToken);
                limitCache[limit.Id] = limit;
            }
        }
    }

    private static async Task PreloadAndUnifyLimitsAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var cardLimitIds = new HashSet<long>();
        var accInfoLimitIds = new HashSet<long>();

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    cardLimitIds.Add(lw.Limit.Id);
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null) continue;
                foreach (var lw in accInfo.Limits)
                {
                    accInfoLimitIds.Add(lw.Limit.Id);
                }
            }
        }

        var existingCardLimits = await unitOfWork.Limit
            .GetListByIdsRawSqlAsync(cardLimitIds.ToList(), cancellationToken);

        var existingAccInfoLimits = await unitOfWork.Limit
            .GetListByIdsRawSqlAsync(accInfoLimitIds.ToList(), cancellationToken);

        var cardLimitCache = existingCardLimits.ToDictionary(l => l.Id, l => l);
        var accInfoLimitCache = existingAccInfoLimits.ToDictionary(l => l.Id, l => l);

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    var lid = lw.Limit.Id;
                    if (cardLimitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                        cardLimitCache[lid] = lw.Limit;
                    }
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null)
                    continue;

                foreach (var lw in accInfo.Limits)
                {
                    var lid = lw.Limit.Id;
                    if (accInfoLimitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                        accInfoLimitCache[lid] = lw.Limit;
                    }
                }
            }
        }
    }

    private static async Task PreloadAndUnifyLimitWrappersAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            await unitOfWork.IssFinAuthAccountsInfo.AddRangeAsync(entity.AccountsInfo, cancellationToken);
            
            if (entity.CardInfo?.Limits != null)
            {
                await unitOfWork.CardInfoLimitWrapper.AddRangeAsync(entity.CardInfo.Limits, cancellationToken);
            }
            
            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null) continue;
                await unitOfWork.AccountsInfoLimitWrapper.AddRangeAsync(accInfo.Limits, cancellationToken);
            }
        }
    }
}