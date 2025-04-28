using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DTOs.OwiUserAction;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для OwiUserAction.
/// </summary>
public class OwiUserActionProcessHandler(
    NotificationEntityMapperFactory mapperFactory,
    IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorOwiUserActionDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, загружает и унифицирует детали,
    /// объединяет расширения и сохраняет сущности в БД.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorOwiUserActionDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = mapperFactory.GetMapper<OwiUserAction, AggregatorOwiUserActionDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)). ToList();
        
        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var entityPreloadService = scope.ServiceProvider.GetRequiredService<IEntityPreloadService>();

        PreloadAndUnifyDetails(entities, unitOfWork);
        await UnifyProcessorExtension<OwiUserAction>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);
        
        entityPreloadService.ProcessEntities(entities);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }
    
    /// <summary>
    /// Загружает и унифицирует детали для сущностей OwiUserAction.
    /// </summary>
    /// <param name="entities">Список сущностей OwiUserAction.</param>
    /// <param name="unitOfWork">Интерфейс для доступа к базе данных.</param>
    private static void PreloadAndUnifyDetails(
        List<OwiUserAction> entities,
        IUnitOfWork unitOfWork)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.OwiUserActionDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = unitOfWork.OwiUserActionDetails
            .GetQueryByIds(allDetailsIds);

        var detailsCache = existingDetailsList.ToDictionary(d => d.OwiUserActionDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.OwiUserActionDetailsId;
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