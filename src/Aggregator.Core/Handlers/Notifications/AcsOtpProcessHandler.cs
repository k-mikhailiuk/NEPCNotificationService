using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DTOs.AcsOtp;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для AcsOtp.
/// </summary>
public class AcsOtpProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorAcsOtpDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности и сохраняет их в БД.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorAcsOtpDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<AcsOtp, AggregatorAcsOtpDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }
    
    /// <summary>
    /// Обрабатывает сущности AcsOtp, добавляя их в БД, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей AcsOtp.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    private static async Task ProcessEntitiesAsync(
        List<AcsOtp> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.AcsOtps
                .GetByIdsAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.AcsOtps.AddWithEncriptionAsync(entity, cancellationToken);
            }
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}