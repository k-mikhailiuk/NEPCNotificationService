using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.Unhold;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для Unhold.
/// </summary>
public class UnholdProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorUnholdDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, загружает и унифицирует детали,
    /// объединяет расширения и сохраняет сущности в базу данных.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO для Unhold.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorUnholdDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = mapperFactory.GetMapper<Unhold, AggregatorUnholdDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();
        
        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var entityPreloadService = scope.ServiceProvider.GetRequiredService<IEntityPreloadService>();

        PreloadAndUnifyDetails(entities, unitOfWork);

        await UnifyProcessorExtension<Unhold>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        entityPreloadService.ProcessEntities(entities);
        
        return entities.Select(x=>x.NotificationId).ToList(); 
    }

    /// <summary>
    /// Загружает и унифицирует детали для сущностей Unhold.
    /// </summary>
    /// <param name="entities">Список сущностей Unhold.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    private static void PreloadAndUnifyDetails(
        List<Unhold> entities,
        IUnitOfWork unitOfWork)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.UnholdDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = unitOfWork.UnholdDetails
            .GetQueryByIds(allDetailsIds);

        var detailsCache = existingDetailsList.ToDictionary(d => d.UnholdDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.UnholdDetailsId;
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