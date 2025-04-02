using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DTOs.AcsOtp;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class AcsOtpProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorAcsOtpDto>, List<long>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public AcsOtpProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<List<long>> Handle(ProcessNotificationCommand<AggregatorAcsOtpDto> request, CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<AcsOtp, AggregatorAcsOtpDto>();

        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
        
        return entities.Select(x=>x.NotificationId).ToList();
    }
    
    private static async Task ProcessEntitiesAsync(
        List<AcsOtp> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            var idsToCheck = new List<long> { entity.NotificationId };

            var existingList = await unitOfWork.AcsOtps
                .GetListByIdsRawSqlAsync(idsToCheck, cancellationToken);

            if (existingList.Count == 0)
            {
                await unitOfWork.AcsOtps.AddWithEncriptionAsync(entity, cancellationToken);
            }
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}