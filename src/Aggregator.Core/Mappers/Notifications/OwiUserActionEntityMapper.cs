using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DTOs.OwiUserAction;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления OwiUserAction (<see cref="AggregatorOwiUserActionDto"/>) в сущность <see cref="OwiUserAction"/>.
/// </summary>
public class OwiUserActionEntityMapper(
    ILogger<OwiUserActionEntityMapper> logger,
    CardInfoMapper cardInfoMapper,
    ConversionExtensionsHelper conversionExtensionsHelper,
    DateTimeConverter dateTimeConverter)
    : INotificationMapper<OwiUserAction, AggregatorOwiUserActionDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorOwiUserActionDto"/> в сущность <see cref="OwiUserAction"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления OwiUserAction.</param>
    /// <returns>Сущность <see cref="OwiUserAction"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если dto равен null.</exception>
    public OwiUserAction Map(AggregatorOwiUserActionDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorBaseOwiUserActionDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorBaseOwiUserActionDto is null");
        }

        var notification = new OwiUserAction
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.OwiUserAction),
            CardInfo = cardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.OwiUserAction,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления (<see cref="AggregatorOwiUserActionDetailsDto"/>) в сущность <see cref="OwiUserActionDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления OwiUserAction.</param>
    /// <returns>Сущность <see cref="OwiUserActionDetails"/> или null, если dto равен null.</returns>
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
            TransactionTime = dateTimeConverter.SafeConvertTime(dto.TransactionTime)
        };
    }
}