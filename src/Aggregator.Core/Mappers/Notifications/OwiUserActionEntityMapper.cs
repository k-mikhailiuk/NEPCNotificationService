using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DTOs.OwiUserAction;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class OwiUserActionEntityMapper : INotificationMapper<OwiUserAction, AggregatorOwiUserActionDto>
{
    private readonly ILogger<OwiUserActionEntityMapper> _logger;

    public OwiUserActionEntityMapper(ILogger<OwiUserActionEntityMapper> logger)
    {
        _logger = logger;
    }

    public OwiUserAction Map(AggregatorOwiUserActionDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("AggregatorOwiUserActionDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorOwiUserActionDto is null");
        }

        var notification = new OwiUserAction
        {
            OwiUserActionId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo)
        };

        return notification;
    }

    private OwiUserActionDetails MapDetails(AggregatorOwiUserActionDetailsDto dto)
    {
        if (dto == null)
        {
            _logger.LogInformation("Details is null");
            return null;
        }

        _logger.LogInformation("Mapping CardStatusChangeDetails:");

        return new OwiUserActionDetails
        {
            Action = dto.Action,
            Login = dto.Login,
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime)
        };
    }
}