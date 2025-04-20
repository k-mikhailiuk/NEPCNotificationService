using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DTOs.AcctBalChange;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления об изменении баланса счета.
/// </summary>
public class
    AcctBalChangeProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorAcctBalChangeDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, 
    /// предварительную загрузку, унификацию и сохранение в БД.
    /// </summary>
    /// <param name="request">Команда с уведомлениями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
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

    /// <summary>
    /// Загружает и унифицирует информацию о картах.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyCardInfoAsync(List<AcctBalChange> entities, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
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

    /// <summary>
    /// Загружает и унифицирует информацию о счетах.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyAccountsInfoAsync(List<AcctBalChange> entities, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
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

    /// <summary>
    /// Обрабатывает сущности изменения баланса счета, добавляя их в БД, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Загружает и унифицирует детали транзакции.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Загружает и унифицирует лимиты.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Загружает и унифицирует обёртки лимитов.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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