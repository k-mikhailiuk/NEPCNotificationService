using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.Unhold;
using Aggregator.Repositories.Abstractions;
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

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<Unhold>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList(); 
    }
    
    /// <summary>
    /// Обрабатывает сущности Unhold, добавляя их в базу данных, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей Unhold.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task ProcessEntitiesAsync(
        List<Unhold> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.Unhold
                .GetListByIdsRawSqlAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.Unhold.AddAsync(entity, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Загружает и унифицирует детали для сущностей Unhold.
    /// </summary>
    /// <param name="entities">Список сущностей Unhold.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyDetailsAsync(
        List<Unhold> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.UnholdDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.UnholdDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);

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