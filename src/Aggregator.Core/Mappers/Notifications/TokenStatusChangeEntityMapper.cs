using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DTOs.TokenStausChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления TokenStatusChange (<see cref="AggregatorTokenStatusChangeDto"/>)
/// в сущность <see cref="TokenStatusChange"/>.
/// </summary>
public class TokenStatusChangeEntityMapper(
    ILogger<TokenStatusChangeEntityMapper> logger,
    CardInfoMapper cardInfoMapper,
    ConversionExtensionsHelper conversionExtensionsHelper,
    DateTimeConverter dateTimeConverter)
    : INotificationMapper<TokenStatusChange, AggregatorTokenStatusChangeDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorTokenStatusChangeDto"/> в сущность <see cref="TokenStatusChange"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления TokenStatusChange.</param>
    /// <returns>Сущность <see cref="TokenStatusChange"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="dto"/> равен null.</exception>
    public TokenStatusChange Map(AggregatorTokenStatusChangeDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorTokenStatusChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorTokenStatusChangeDto is null");
        }

        var notification = new TokenStatusChange
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.TokenStatusChange),
            CardInfo = cardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.TokenStatusChange,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления TokenStatusChange (<see cref="AggregatorTokenStatusChangeDetailsDto"/>)
    /// в сущность <see cref="TokenStatusChangeDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления TokenStatusChange.</param>
    /// <returns>
    /// Сущность <see cref="TokenStatusChangeDetails"/> с заполненными данными или null, если dto равен null.
    /// </returns>
    private TokenStatusChangeDetails MapDetails(AggregatorTokenStatusChangeDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("Details is null");
            return null;
        }

        logger.LogInformation("Mapping TokenStatusChangeDetails");

        return new TokenStatusChangeDetails
        {
            DpanRef = dto.DpanRef,
            PaymentSystem = dto.PaymentSystem,
            Status = dto.Status,
            ChangeDate = dateTimeConverter.SafeConvertTime(dto.ChangeDate),
            DpanExpDate = dto.DpanExpDate,
            WalletProvider = dto.WalletProvider,
            DeviceName = dto.DeviceName,
            DeviceId = dto.DeviceId,
            FpanRef = dto.FpanRef,
            CardIdentifier = conversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}