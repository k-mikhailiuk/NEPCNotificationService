using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs.CardStatusChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления CardStatusChange (<see cref="AggregatorCardStatusChangeDto"/>) в сущность <see cref="CardStatusChange"/>.
/// </summary>
public class CardStatusChangeEntityMapper(ILogger<CardStatusChangeEntityMapper> logger)
    : INotificationMapper<CardStatusChange, AggregatorCardStatusChangeDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorCardStatusChangeDto"/> в сущность <see cref="CardStatusChange"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления CardStatusChange.</param>
    /// <returns>Сущность <see cref="CardStatusChange"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="dto"/> равен null.</exception>
    public CardStatusChange Map(AggregatorCardStatusChangeDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorCardStatusChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");
        }

        var notification = new CardStatusChange
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.CardStatusChange),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.CardStatusChange,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления в сущность <see cref="CardStatusChangeDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления.</param>
    /// <returns>Сущность <see cref="CardStatusChangeDetails"/> с заполненными данными или null, если dto равен null.</returns>
    private CardStatusChangeDetails MapDetails(AggregatorCardStatusChangeDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("Details is null");
            return null;
        }

        logger.LogInformation("Mapping CardStatusChangeDetails:");

        return new CardStatusChangeDetails
        {
            ExpDate = dto.ExpDate,
            OldStatus = dto.OldStatus,
            NewStatus = dto.NewStatus,
            ChangeDate = ConversionExtensionsHelper.SafeConvertTime(dto.ChangeDate),
            Service = dto.Service,
            UserName = dto.UserName,
            Note = dto.Note,
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier),
        };
    }
}