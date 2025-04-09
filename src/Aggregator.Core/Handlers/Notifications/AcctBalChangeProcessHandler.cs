using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class
    AcctBalChangeProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorAcctBalChangeDto>, List<long>>
{
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorAcctBalChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<AcctBalChange, AggregatorAcctBalChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);
        
        await PreloadAndUnifyLimitWrappersAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyLimitsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<AcctBalChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);

        return entities.Select(x => x.NotificationId).ToList();
    }

    private static async Task ProcessEntitiesAsync(
        List<AcctBalChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.AcctBalChange
                .GetListByIdsRawSqlAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.AcctBalChange.AddAsync(entity, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task PreloadAndUnifyDetailsAsync(
        List<AcctBalChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.AcctBalChangeDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.AcctBalChangeDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);

        var finTransIds = entities
            .Select(e => e.Details.FinTrans.FinTransactionId)
            .Distinct()
            .ToList();

        var existingTrans = await unitOfWork.FinTransaction
            .GetListByIdsRawSqlAsync(finTransIds, cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.AcctBalChangeDetailsId, d => d);
        var finTransCache = existingTrans.ToDictionary(ft => ft.FinTransactionId, ft => ft);

        foreach (var entity in entities)
        {
            var ftId = entity.Details.FinTrans.FinTransactionId;
            if (finTransCache.TryGetValue(ftId, out var existingFinTrans))
            {
                entity.Details.FinTrans = existingFinTrans;
                unitOfWork.Attach(existingFinTrans);
            }
            else
            {
                finTransCache[ftId] = entity.Details.FinTrans;
            }

            var dId = entity.Details.AcctBalChangeDetailsId;
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


    private static async Task PreloadAndUnifyLimitsAsync(
        List<AcctBalChange> entities,
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
                    limitIds.Add(lw.Limit.Id);
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null) continue;
                foreach (var lw in accInfo.Limits)
                {
                    limitIds.Add(lw.Limit.Id);
                }
            }
        }

        var existingLimits = await unitOfWork.Limit
            .GetListByIdsRawSqlAsync(limitIds.ToList(), cancellationToken);

        var limitCache = existingLimits.ToDictionary(l => l.Id, l => l);

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    var lid = lw.Limit.Id;
                    if (limitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
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
                    var lid = lw.Limit.Id;
                    if (limitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                        limitCache[lid] = lw.Limit;
                    }
                }
            }
        }
    }
    
        private static async Task PreloadAndUnifyLimitWrappersAsync(
        List<AcctBalChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var cardInfoWrapper = new HashSet<long>();
        var accointsInfoWrapper = new HashSet<long>();

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    cardInfoWrapper.Add(lw.Id);
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null) continue;
                foreach (var lw in accInfo.Limits)
                {
                    accointsInfoWrapper.Add(lw.Id);
                }
            }
        }

        var existingCardWrappers = await unitOfWork.CardInfoLimitWrapper
            .GetListByIdsRawSqlAsync(cardInfoWrapper.ToList(), cancellationToken);

        var existingAccInfoWrappers = await unitOfWork.AccountsInfoLimitWrapper
            .GetListByIdsRawSqlAsync(accointsInfoWrapper.ToList(), cancellationToken);

        var cardWrappersCache = existingCardWrappers.ToDictionary(l => l.Id, l => l);
        var accInfoWrappersCache = existingAccInfoWrappers.ToDictionary(l => l.Id, l => l);

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    var lwid = lw.Id;

                    if (cardWrappersCache.TryGetValue(lwid, out var existingLw)) continue;

                    await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                    cardWrappersCache[lwid] = lw;
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null)
                    continue;

                foreach (var lw in accInfo.Limits)
                {
                    var lid = lw.Limit.Id;
                    if (accInfoWrappersCache.TryGetValue(lid, out var existingLw)) continue;

                    await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                    accInfoWrappersCache[lid] = lw;
                }
            }
        }
    }
}