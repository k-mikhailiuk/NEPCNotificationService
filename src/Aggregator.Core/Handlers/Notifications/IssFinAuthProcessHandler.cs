using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Handlers.Notifications;

public class IssFinAuthProcessHandler : IRequestHandler<ProcessNotificationCommand<AggregatorIssFinAuthDto>>
{
    private readonly NotificationEntityMapperFactory _mapperFactory;
    private readonly IServiceProvider _serviceProvider;

    public IssFinAuthProcessHandler(NotificationEntityMapperFactory mapperFactory, IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();

        var entities = dtos.Select(dto => mapper.Map(dto));

        await ProcessEntitiesAsync(entities, cancellationToken);

        throw new NotImplementedException();
    }

    private async Task ProcessEntitiesAsync(IEnumerable<IssFinAuth> entities, CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            foreach (var entity in entities)
            {
                await ProcessCardInfoAsync(entity.CardInfo, unitOfWork, cancellationToken);

                await unitOfWork.MerchantInfo.AddAsync(entity.MerchantInfo, cancellationToken);

                await ProcessDetailsAsync(entity.Details, unitOfWork, cancellationToken);
                
                await ProcessAccountsInfo(entity.AccountsInfo, unitOfWork, cancellationToken);
                
                await unitOfWork.IssFinAuth.AddAsync(entity, cancellationToken);
                
                await ProcessExtensionsAsync(entity.Extensions, unitOfWork, cancellationToken);

                await unitOfWork.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task ProcessCardInfoAsync(CardInfo? cardInfoEntity, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (cardInfoEntity?.Limits != null)
        {
            foreach (var limit in cardInfoEntity.Limits)
            {
                var existingLimit = await unitOfWork.Limit.GetByIdAsync(limit.Limit.LimitId, cancellationToken);

                if (existingLimit == null)
                    await unitOfWork.Limit.AddAsync(limit.Limit, cancellationToken);
            }
        }
    }

    private async Task ProcessDetailsAsync(IssFinAuthDetails issFinAuthDetails, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (issFinAuthDetails.CheckedLimits != null)
        {
            foreach (var limit in issFinAuthDetails.CheckedLimits)
            {
                var existingLimit = await unitOfWork.Limit.GetByIdAsync(limit.Id, cancellationToken);

                if (existingLimit == null)
                    await unitOfWork.CheckedLimit.AddAsync(limit, cancellationToken);
            }
        }
        
        var existingDetails = await unitOfWork.IssFinAuthDetails.GetByIdAsync(issFinAuthDetails.IssFinAuthDetailsId, cancellationToken);
        if (existingDetails == null)
            await unitOfWork.IssFinAuthDetails.AddAsync(issFinAuthDetails, cancellationToken);
    }

    private async Task ProcessExtensionsAsync(List<NotificationExtension> extensions, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if(!extensions.Any())
            return;
        
        foreach (var extension in extensions)
        {
            if (extension.ExtesionParameters != null)
            {
                foreach (var parameter in extension.ExtesionParameters)
                {
                    await unitOfWork.ExtensionParameter.AddAsync(parameter, cancellationToken);
                }
            }
            await unitOfWork.NotificationExtension.AddAsync(extension, cancellationToken);
        }
    }

    private async Task ProcessAccountsInfo(List<IssFinAuthAccountsInfo> issFinAuthAccountsInfo, IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var accountInfo in issFinAuthAccountsInfo)
        {
            if (accountInfo.Limits != null)
            {
                foreach (var accountsInfoLimitWrapper in accountInfo.Limits)
                {
                    var limit = accountsInfoLimitWrapper.Limit;

                    var existingLimit = await unitOfWork.Limit.GetByIdAsync(limit.LimitId, cancellationToken);

                    if (existingLimit != null)
                    {
                        accountsInfoLimitWrapper.Limit = existingLimit;
                    }
                    else
                    {
                        await unitOfWork.Limit.AddAsync(limit, cancellationToken);
                        await unitOfWork.AccountsInfoLimitWrapper.AddAsync(accountsInfoLimitWrapper, cancellationToken);
                    }
                }
            }
            await unitOfWork.IssFinAuthAccountsInfo.AddAsync(accountInfo, cancellationToken);
        }
    }
}