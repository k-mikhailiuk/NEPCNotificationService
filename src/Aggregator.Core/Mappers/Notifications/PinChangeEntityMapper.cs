using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class PinChangeEntityMapper(ILogger<PinChangeEntityMapper> logger)
    : INotificationMapper<PinChange, AggregatorPinChangeDto>
{
    public PinChange Map(AggregatorPinChangeDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorPinChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorPinChangeDto is null");

        }

        var notification = new PinChange
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.PinChange),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.PinChange,
        };

        return notification;
    }

    private PinChangeDetails MapDetails(AggregatorPinChangeDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("PinChangeDetailsDto is null");
            return null;
        }

        logger.LogInformation($"Mapping PinChangeDetails: TransactionTime={dto.TransactionTime}", dto.TransactionTime);

        return new PinChangeDetails()
        {
            ExpDate = dto.ExpDate,
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime),
            Status = dto.Status,
            ResponseCode = dto.ResponseCode,
            Service = dto.Service,
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}