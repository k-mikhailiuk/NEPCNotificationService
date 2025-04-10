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
        
        await PreloadAndUnifyLimitsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyAccountsInfoAsync(entities, unitOfWork, cancellationToken);
        await PreloadAndUnifyCardInfoAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyLimitWrappersAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<AcctBalChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);

        return entities.Select(x => x.NotificationId).ToList();
    }

    private static async Task PreloadAndUnifyCardInfoAsync(List<AcctBalChange> entities, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        var allCardInfoIds = new List<long>();
        foreach (var entity in entities)
        {
            if (entity.CardInfo != null)
                allCardInfoIds.Add(entity.CardInfo.Id);
        }

        if (allCardInfoIds.Count == 0)
            return;

        var existingCardInfos = await unitOfWork.CardInfo
            .GetListByIdsRawSqlAsync(allCardInfoIds, cancellationToken);

        var cardInfoCache = existingCardInfos.ToDictionary(m => m.Id, m => m);

        foreach (var cardInfo in entities.Select(e => e.CardInfo))
        {
            var mid = cardInfo.Id;

            if (cardInfoCache.TryGetValue(mid, out _)) continue;

            cardInfoCache[mid] = cardInfo;
            await unitOfWork.CardInfo.AddAsync(cardInfo, cancellationToken);
        }
    }

    private static async Task PreloadAndUnifyAccountsInfoAsync(List<AcctBalChange> entities, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        var allAccountsInfoIds = entities.SelectMany(e => e.AccountsInfo.Select(a => a.Id)).ToList();

        var existingAccountInfos = await unitOfWork.AccountsInfos
            .GetListByIdsRawSqlAsync(allAccountsInfoIds, cancellationToken);

        var accountInfoCache = existingAccountInfos.ToDictionary(m => m.Id, m => m);

        foreach (var accountsInfo in entities.SelectMany(e => e.AccountsInfo))
        {
            var mid = accountsInfo.Id;

            if (accountInfoCache.TryGetValue(mid, out _)) continue;

            accountInfoCache[mid] = accountsInfo;
            await unitOfWork.AccountsInfos.AddAsync(accountsInfo, cancellationToken);
        }
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
        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null)
                    continue;

                foreach (var lw in accInfo.Limits)
                {
                    await unitOfWork.Limit.AddAsync(lw.Limit, cancellationToken);
                }
            }
        }
    }

    private static async Task PreloadAndUnifyLimitWrappersAsync(
        List<AcctBalChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var accountsInfo in entities.SelectMany(entity => entity.AccountsInfo))
        {
            if (accountsInfo.Limits != null)
                await unitOfWork.AccountsInfoLimitWrapper.AddRangeAsync(accountsInfo.Limits, cancellationToken);
        }

        foreach (var cardInfos in entities.Select(entity => entity.CardInfo))
        {
            if (cardInfos.Limits != null)
                await unitOfWork.CardInfoLimitWrapper.AddRangeAsync(cardInfos.Limits, cancellationToken);
        }
    }
}