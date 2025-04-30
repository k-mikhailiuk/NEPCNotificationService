using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DTOs.CardStatusChange;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для изменения статуса карты.
/// </summary>
public class CardStatusChangeProcessHandler(
    NotificationEntityMapperFactory mapperFactory,
    IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorCardStatusChangeDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, загружает и унифицирует детали, 
    /// объединяет расширения и сохраняет сущности в БД.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorCardStatusChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<CardStatusChange, AggregatorCardStatusChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IAggregatorUnitOfWork>();
        var entityPreloadService = scope.ServiceProvider.GetRequiredService<IEntityPreloadService>();

        PreloadAndUnifyDetails(entities, unitOfWork);
        await UnifyProcessorExtension<CardStatusChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);
        
        entityPreloadService.ProcessEntities(entities);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    /// <summary>
    /// Загружает и унифицирует детали для сущностей изменения статуса карты.
    /// </summary>
    /// <param name="entities">Список сущностей <see cref="CardStatusChange"/>.</param>
    /// <param name="aggregatorUnitOfWork">Интерфейс для работы с базой данных.</param>
    private static void PreloadAndUnifyDetails(
        List<CardStatusChange> entities,
        IAggregatorUnitOfWork aggregatorUnitOfWork)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.CardStatusChangeDetailsId)
            .Distinct()
            .ToList();
        
        var existingDetailsList = aggregatorUnitOfWork.CardStatusChangeDetails
            .GetQueryByIds(allDetailsIds);

        var detailsCache = existingDetailsList.ToDictionary(d => d.CardStatusChangeDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.CardStatusChangeDetailsId;
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