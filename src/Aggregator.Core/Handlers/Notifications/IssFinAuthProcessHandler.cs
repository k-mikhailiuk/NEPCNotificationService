using Aggregator.Core.Commands;
using Aggregator.Core.Mappers;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
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

    public IssFinAuthProcessHandler(
        NotificationEntityMapperFactory mapperFactory,
        IServiceProvider serviceProvider)
    {
        _mapperFactory = mapperFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(ProcessNotificationCommand<AggregatorIssFinAuthDto> request,
        CancellationToken cancellationToken)
    {
        var dtos = request.Notifications;

        var mapper = _mapperFactory.GetMapper<IssFinAuth, AggregatorIssFinAuthDto>();
        var entities = dtos.Select(dto => mapper.Map(dto)).ToList();

        using var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await PreloadAndUnifyDetailsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyMerchantAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyLimitsAsync(entities, unitOfWork, cancellationToken);

        await PreloadAndUnifyExtensionsAsync(entities, unitOfWork, cancellationToken);

        await ProcessEntitiesAsync(entities, unitOfWork, cancellationToken);
    }

    private async Task ProcessEntitiesAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            await ProcessDetailsLimitsAsync(entity.Details, unitOfWork, cancellationToken);

            var existing = await unitOfWork.IssFinAuth.GetByIdAsync(entity.NotificationId, cancellationToken);
            if (existing == null)
            {
                await unitOfWork.IssFinAuth.AddAsync(entity, cancellationToken);
            }

            await ProcessExtensionsAsync(entity.Extensions, unitOfWork, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync();
    }

    private async Task PreloadAndUnifyDetailsAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allDetailsIds = entities
            .Select(e => e.Details.IssFinAuthDetailsId)
            .Distinct()
            .ToList();

        var existingDetailsList = await unitOfWork.IssFinAuthDetails
            .GetListAsync(d => allDetailsIds.Contains(d.IssFinAuthDetailsId), cancellationToken);

        var detailsCache = existingDetailsList.ToDictionary(d => d.IssFinAuthDetailsId, d => d);

        foreach (var entity in entities)
        {
            var dId = entity.Details.IssFinAuthDetailsId;
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

    private async Task PreloadAndUnifyMerchantAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allMerchantIds = entities
            .Where(e => e.MerchantInfo != null)
            .Select(e => e.MerchantInfo!.Id)
            .Distinct()
            .ToList();

        var existingMerchants = await unitOfWork.MerchantInfo
            .GetListAsync(m => allMerchantIds.Contains(m.Id), cancellationToken);

        var merchantCache = existingMerchants.ToDictionary(m => m.Id, m => m);

        foreach (var entity in entities)
        {
            if (entity.MerchantInfo == null)
                continue;

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

    private async Task ProcessDetailsLimitsAsync(
        IssFinAuthDetails issFinAuthDetails,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        if (issFinAuthDetails.CheckedLimits == null)
            return;

        foreach (var limit in issFinAuthDetails.CheckedLimits)
        {
            var existingLimit = await unitOfWork.Limit.FindAsync(
                x => x.LimitId == limit.Id, cancellationToken);

            if (existingLimit == null)
            {
                await unitOfWork.CheckedLimit.AddAsync(limit, cancellationToken);
            }
        }
    }

    private async Task ProcessExtensionsAsync(
        List<NotificationExtension> extensions,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        foreach (var extension in extensions)
        {
            if (extension.ExtensionParameters != null)
            {
                foreach (var parameter in extension.ExtensionParameters)
                {
                    await unitOfWork.ExtensionParameter.AddAsync(parameter, cancellationToken);
                }
            }
        }
    }


    private async Task PreloadAndUnifyLimitsAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var limitIds = new HashSet<long>();

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    limitIds.Add(lw.Limit.LimitId);
                }
            }

            if (entity.AccountsInfo != null)
            {
                foreach (var accInfo in entity.AccountsInfo)
                {
                    if (accInfo.Limits == null) continue;
                    foreach (var lw in accInfo.Limits)
                    {
                        limitIds.Add(lw.Limit.LimitId);
                    }
                }
            }
        }

        var existingLimits = await unitOfWork.Limit
            .GetListAsync(l => limitIds.Contains(l.LimitId), cancellationToken);

        var limitCache = existingLimits.ToDictionary(l => l.LimitId, l => l);

        foreach (var entity in entities)
        {
            if (entity.CardInfo?.Limits != null)
            {
                foreach (var lw in entity.CardInfo.Limits)
                {
                    var lid = lw.Limit.LimitId;
                    if (limitCache.TryGetValue(lid, out var existingLim))
                    {
                        lw.Limit = existingLim;
                    }
                    else
                    {
                        limitCache[lid] = lw.Limit;
                    }
                }
            }

            if (entity.AccountsInfo != null)
            {
                foreach (var accInfo in entity.AccountsInfo)
                {
                    if (accInfo.Limits == null)
                        continue;

                    foreach (var lw in accInfo.Limits)
                    {
                        var lid = lw.Limit.LimitId;
                        if (limitCache.TryGetValue(lid, out var existingLim))
                        {
                            lw.Limit = existingLim;
                        }
                        else
                        {
                            limitCache[lid] = lw.Limit;
                        }
                    }
                }
            }
        }
    }

    private async Task PreloadAndUnifyExtensionsAsync(
        List<IssFinAuth> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var allExtensionIds = new HashSet<string>();

        foreach (var entity in entities)
        {
            if (entity.Extensions == null) continue;

            foreach (var ext in entity.Extensions)
            {
                allExtensionIds.Add(ext.ExtensionId);
            }
        }

        var existingExtensions = await unitOfWork.NotificationExtension
            .GetListAsync(
                x => allExtensionIds.Contains(x.ExtensionId)
                     && x.NotificationType == NotificationType.IssFinAuth,
                cancellationToken
            );

        var extCache = existingExtensions.ToDictionary(x => x.ExtensionId, x => x);

        foreach (var issFinAuth in entities)
        {
            if (issFinAuth.Extensions == null) continue;
            for (var i = 0; i < issFinAuth.Extensions.Count; i++)
            {
                var ext = issFinAuth.Extensions[i];
                if (extCache.TryGetValue(ext.ExtensionId, out var existingExt))
                {
                    issFinAuth.Extensions[i] = existingExt;
                }
                else
                {
                    extCache[ext.ExtensionId] = ext;
                }
            }
        }
    }
}