using Aggregator.Core.Commands;
using Aggregator.Core.Extensions;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DTOs.AcqFinAuth;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для AcqFinAuth.
/// </summary>
public class AcqFinAuthProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorAcqFinAuthDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности, загрузку и унификацию деталей и мерчанта,
    /// а также сохранение данных в БД.
    /// </summary>
    /// <param name="request">Команда уведомления с DTO.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorAcqFinAuthDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;
        
        var mapper = mapperFactory.GetMapper<AcqFinAuth, AggregatorAcqFinAuthDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();
        
        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);
        await PreloadAndUnifyMerchantAsync(entities, unitOfWork, cancellationToken);
        await UnifyProcessorExtension<AcqFinAuth>.PreloadAndUnifyExtensionsAsync(entities, unitOfWork, cancellationToken);
        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }
    
    /// <summary>
    /// Обрабатывает сущности AcqFinAuth, добавляя их в БД, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей AcqFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для БД.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task ProcessEntitiesAsync(
        List<AcqFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.AcqFinAuth
                .GetByIdsAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                unitOfWork.AcqFinAuth.Add(entity);
            }
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
    
    /// <summary>
    /// Загружает и унифицирует детали транзакции для сущностей AcqFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей AcqFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для БД.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyDetailsAsync(
        List<AcqFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.AcqFinAuthDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.AcqFinAuthDetails
            .GetListByIdsRawSqlAsync(allDetailsIds, cancellationToken);
        
        var detailsCache = existingDetailsList.ToDictionary(d => d.AcqFinAuthDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.AcqFinAuthDetailsId;
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
    /// Загружает и унифицирует информацию о мерчанте для сущностей AcqFinAuth.
    /// </summary>
    /// <param name="entities">Список сущностей AcqFinAuth.</param>
    /// <param name="unitOfWork">Интерфейс единицы работы для БД.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task PreloadAndUnifyMerchantAsync(
        List<AcqFinAuth> entities,
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
}