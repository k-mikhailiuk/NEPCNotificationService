using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для изменения PIN-кода.
/// </summary>
public class PinChangeProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorPinChangeDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, загружает и унифицирует детали,
    /// объединяет расширения и сохраняет сущности в базу данных.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO для изменения PIN-кода.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorPinChangeDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<PinChange, AggregatorPinChangeDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IAggregatorUnitOfWork>();
        var entityPreloadService = scope.ServiceProvider.GetRequiredService<IEntityPreloadService>();

        PreloadAndUnifyDetails(entities, unitOfWork);

        await UnifyProcessorExtension<PinChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        entityPreloadService.ProcessEntities(entities);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    /// <summary>
    /// Загружает и унифицирует детали для сущностей PinChange.
    /// </summary>
    /// <param name="entities">Список сущностей изменения PIN-кода.</param>
    /// <param name="aggregatorUnitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyDetails(
        List<PinChange> entities,
        IAggregatorUnitOfWork aggregatorUnitOfWork)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.PinChangeDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = aggregatorUnitOfWork.PinChangeDetails
            .GetQueryByIds(allDetailsIds);

        var detailsCache = existingDetailsList.ToDictionary(d => d.PinChangeDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.PinChangeDetailsId;
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