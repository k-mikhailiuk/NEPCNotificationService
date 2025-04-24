using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var entityPreloadService = scope.ServiceProvider.GetRequiredService<IEntityPreloadService>();

        PreloadAndUnifyLimits(entities, unitOfWork);

        PreloadAndUnifyDetails(entities, unitOfWork);

        PreloadAndUnifyMerchant(entities, unitOfWork);

        PreloadAndUnifyAccountsInfo(entities, unitOfWork);
        PreloadAndUnifyCardInfo(entities, unitOfWork);

        PreloadAndUnifyLimitWrappers(entities, unitOfWork);
        
        await UnifyProcessorExtension<IssFinAuth>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);
        
        foreach (var entity in entities)
            ProcessDetailsLimits(entity.Details, unitOfWork);

        entityPreloadService.ProcessEntities(entities);

        return entities.Select(x => x.NotificationId).ToList();
    }

    /// <summary>
    /// Загружает и унифицирует информацию о картах для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyCardInfo(List<IssFinAuth> entities, IUnitOfWork unitOfWork)
    {
        var allCardInfoIds = new List<long>();
        foreach (var entity in entities)
        {
            if (entity.CardInfo != null)
                allCardInfoIds.Add(entity.CardInfo.Id);
        }

        if (allCardInfoIds.Count == 0)
            return;

        var existingCardInfos = unitOfWork.CardInfo
            .GetQueryByIds(allCardInfoIds);

        var cardInfoCache = existingCardInfos.ToDictionary(m => m.Id, m => m);

        foreach (var cardInfo in entities.Select(e => e.CardInfo))
        {
            var mid = cardInfo.Id;

            if (cardInfoCache.TryGetValue(mid, out _)) continue;

            cardInfoCache[mid] = cardInfo;
            unitOfWork.CardInfo.Add(cardInfo);
        }
    }

    /// <summary>
    /// Загружает и унифицирует информацию о счетах для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyAccountsInfo(List<IssFinAuth> entities, IUnitOfWork unitOfWork)
    {
        var allAccountsInfoIds = entities.SelectMany(e => e.AccountsInfo.Select(a => a.Id)).ToList();

        var existingAccountInfos = unitOfWork.AccountsInfos
            .GetQueryByIds(allAccountsInfoIds);

        var accountInfoCache = existingAccountInfos.ToDictionary(m => m.Id, m => m);

        foreach (var accountsInfo in entities.SelectMany(e => e.AccountsInfo))
        {
            var mid = accountsInfo.Id;

            if (accountInfoCache.TryGetValue(mid, out _)) continue;

            accountInfoCache[mid] = accountsInfo;
            unitOfWork.AccountsInfos.Add(accountsInfo);
        }
    }

    /// <summary>
    /// Загружает и унифицирует обёртки лимитов для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyLimitWrappers(List<IssFinAuth> entities, IUnitOfWork unitOfWork)
    {
        foreach (var accountsInfo in entities.SelectMany(entity => entity.AccountsInfo))
        {
            if (accountsInfo.Limits != null)
                unitOfWork.AccountsInfoLimitWrapper.AddRange(accountsInfo.Limits);
        }

        foreach (var cardInfos in entities.Select(entity => entity.CardInfo))
        {
            if (cardInfos.Limits != null)
                unitOfWork.CardInfoLimitWrapper.AddRange(cardInfos.Limits);
        }
    }

    /// <summary>
    /// Загружает и унифицирует детали для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyDetails(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.IssFinAuthDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = unitOfWork.IssFinAuthDetails
            .GetQueryByIds(allDetailsIds);

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
    private static void PreloadAndUnifyMerchant(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork)
    {
        var allMerchantIds = entities
            .Select(e => e.MerchantInfo.Id)
            .Distinct()
            .ToList();

        var existingMerchants =  unitOfWork.MerchantInfo
            .GetQueryByIds(allMerchantIds);

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
    private static void ProcessDetailsLimits(
        IssFinAuthDetails issFinAuthDetails,
        IUnitOfWork unitOfWork)
    {
        if (issFinAuthDetails.CheckedLimits == null || issFinAuthDetails.CheckedLimits.Count == 0)
            return;

        var allLimitIds = issFinAuthDetails.CheckedLimits
            .Select(l => l.Id)
            .Distinct()
            .ToList();

        var existingLimits = unitOfWork.CheckedLimit
            .GetQueryByIds(allLimitIds);
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
                unitOfWork.CheckedLimit.Add(limit);
                limitCache[limit.Id] = limit;
            }
        }
    }

    /// <summary>
    /// Загружает и унифицирует лимиты для сущностей IssFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей IssFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyLimits(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork)
    {
        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    unitOfWork.Limit.Add(lw.Limit);
                }
            }

            foreach (var accInfo in entity.AccountsInfo)
            {
                if (accInfo.Limits == null)
                    continue;

                foreach (var lw in accInfo.Limits)
                {
                    unitOfWork.Limit.Add(lw.Limit);
                }
            }
        }
    }
}