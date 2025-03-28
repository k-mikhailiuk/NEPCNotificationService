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
    AcctBalChangeProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcctBalChangeDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public AcctBalChangeProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorAcctBalChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<AcctBalChange, AggregatorAcctBalChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

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