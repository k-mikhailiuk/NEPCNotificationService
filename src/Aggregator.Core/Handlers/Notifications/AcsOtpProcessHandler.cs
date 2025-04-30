using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DTOs.AcsOtp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

/// <summary>
/// Обработчик команды уведомления для AcsOtp.
/// </summary>
public class AcsOtpProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    : IRequestHandler<ProcessNotificationCommand<AggregatorOtpDto>, List<long>>
{
    /// <summary>
    /// Обрабатывает команду уведомления, выполняет маппинг DTO в сущности и сохраняет их в БД.
    /// </summary>
    /// <param name="request">Команда уведомления с коллекцией DTO.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список идентификаторов уведомлений.</returns>
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorOtpDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = mapperFactory.GetMapper<AcsOtp, AggregatorOtpDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        ProcessEntities(entities, unitOfWork);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }
    
    /// <summary>
    /// Обрабатывает сущности AcsOtp, добавляя их в БД, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей AcsOtp.</param>
    /// <param name="unitOfWork">Интерфейс для работы с базой данных.</param>
    private static void ProcessEntities(
        List<AcsOtp> entities,
        IUnitOfWork unitOfWork)
    {
        var idsToCheck = (IReadOnlyCollection<long>)entities.Select(x=>x.NotificationId);

        var existingList = unitOfWork.AcsOtps
            .GetQueryByIds(idsToCheck).Select(x=>x.NotificationId);
        
        foreach (var entity in entities.Where(entity => !existingList.Contains(entity.NotificationId)))
        {
            unitOfWork.AcsOtps.AddWithEncryption(entity);
        }
    }
}