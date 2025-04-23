using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
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
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await UnifyProcessorExtension<PinChange>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork,
            cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }

    /// <summary>
    /// Обрабатывает сущности PinChange, добавляя их в базу данных, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей изменения PIN-кода.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task ProcessEntitiesAsync(
        List<PinChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.PinChange
                .GetByIdsAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.PinChange.AddAsync(entity, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Загружает и унифицирует детали для сущностей PinChange.
    /// </summary>
    /// <param name="entities">Список сущностей изменения PIN-кода.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для доступа к базе данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyDetailsAsync(
        List<PinChange> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.PinChangeDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.PinChangeDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);

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