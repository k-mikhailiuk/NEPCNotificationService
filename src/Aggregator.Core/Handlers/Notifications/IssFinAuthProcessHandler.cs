using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для IssFinAuth.
/// </summary>
public class IssFinAuthProcessHandler(
    NotificationEntityMapperFactory mapperFactory,
    IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, загружает и унифицирует лимиты, детали, мерчанта, информацию о счетах и картах,
    /// объединяет обёртки лимитов и расширения, а затем сохраняет сущности в базу данных.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();
        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyLimitsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyMerchantAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyAccountsInfoAsync(entities, unitOfWork, cancellationToken);
        await PreloadAndUnifyCardInfoAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyLimitWrappersAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<IssFinAuth>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);

        return entities.Select(x => x.NotificationId).ToList();
    }

    /// <summary>
    /// Загружает и унифицирует информацию о картах для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyCardInfoAsync(List<IssFinAuth> entities, IUnitOfWork unitOfWork,
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
    /// Загружает и унифицирует информацию о счетах для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyAccountsInfoAsync(List<IssFinAuth> entities, IUnitOfWork unitOfWork,
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
    /// Загружает и унифицирует обёртки лимитов для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyLimitWrappersAsync(List<IssFinAuth> entities, IUnitOfWork unitOfWork,
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

    /// <summary>
    /// Обрабатывает сущности IssFinAuth, включая обновление лимитов деталей, и сохраняет их в базе данных.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Загружает и унифицирует детали для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Загружает и унифицирует информацию о мерчанте для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Обрабатывает лимиты деталей IssFinAuth.
    /// </summary>
    /// <param name="issFinAuthDetails">Детали IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
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

    /// <summary>
    /// Загружает и унифицирует лимиты для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyLimitsAsync(
        List<IssFinAuth> entities,
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
}