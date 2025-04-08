using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DTOs.OwiUserAction;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class OwiUserActionEntityMapper(ILogger<OwiUserActionEntityMapper> logger)
    : INotificationMapper<OwiUserAction, AggregatorOwiUserActionDto>
{
    public OwiUserAction Map(AggregatorOwiUserActionDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorOwiUserActionDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorOwiUserActionDto is null");
        }

        var notification = new OwiUserAction
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.OwiUserAction),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.OwiUserAction,
        };

        return notification;
    }

    private OwiUserActionDetails MapDetails(AggregatorOwiUserActionDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("Details is null");
            return null;
        }

        logger.LogInformation("Mapping CardStatusChangeDetails:");

        return new OwiUserActionDetails
        {
            Action = dto.Action,
            Login = dto.Login,
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime)
        };
    }
}