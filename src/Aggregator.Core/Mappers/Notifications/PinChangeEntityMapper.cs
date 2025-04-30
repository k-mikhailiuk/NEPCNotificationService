using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления PinChange (<see cref="AggregatorPinChangeDto"/>) в сущность <see cref="PinChange"/>.
/// </summary>
public class PinChangeEntityMapper(
    ILogger<PinChangeEntityMapper> logger,
    CardInfoMapper cardInfoMapper,
    ConversionExtensionsHelper conversionExtensionsHelper,
    DateTimeConverter dateTimeConverter)
    : INotificationMapper<PinChange, AggregatorPinChangeDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorPinChangeDto"/> в сущность <see cref="PinChange"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления изменения PIN-кода.</param>
    /// <returns>Сущность <see cref="PinChange"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="dto"/> равен null.</exception>
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
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.PinChange),
            CardInfo = cardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.PinChange,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления (<see cref="AggregatorPinChangeDetailsDto"/>) в сущность <see cref="PinChangeDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей изменения PIN-кода.</param>
    /// <returns>Сущность <see cref="PinChangeDetails"/> или null, если dto равен null.</returns>
    private PinChangeDetails? MapDetails(AggregatorPinChangeDetailsDto? dto)
    {
        if (dto == null)
        {
            logger.LogInformation("PinChangeDetailsDto is null");
            return null;
        }

        logger.LogInformation($"Mapping PinChangeDetails: TransactionTime={dto.TransactionTime}", dto.TransactionTime);

        return new PinChangeDetails
        {
            ExpDate = dto.ExpDate,
            TransactionTime = dateTimeConverter.SafeConvertTime(dto.TransactionTime),
            Status = dto.Status,
            ResponseCode = dto.ResponseCode,
            Service = dto.Service,
            CardIdentifier = conversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}