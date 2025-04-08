using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DTOs.TokenStausChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class TokenStatusChangeEntityMapper(ILogger<TokenStatusChangeEntityMapper> logger)
    : INotificationMapper<TokenStatusChange, AggregatorTokenStatusChangeDto>
{
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
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.TokenStatusChange),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.TokenStatusChange,
        };

        return notification;
    }

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
            ChangeDate = ConversionExtensionsHelper.SafeConvertTime(dto.ChangeDate),
            DpanExpDate = dto.DpanExpDate,
            WalletProvider = dto.WalletProvider,
            DeviceName = dto.DeviceName,
            DeviceId = dto.DeviceId,
            FpanRef = dto.FpanRef,
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}