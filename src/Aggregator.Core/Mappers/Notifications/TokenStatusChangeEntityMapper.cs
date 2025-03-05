using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DTOs.TokenStausChange;

namespace Aggregator.Core.Mappers.Notifications;

public class TokenStatusChangeEntityMapper : INotificationMapper<TokenStatusChange, AggregatorTokenStatusChangeDto>
{
    public TokenStatusChange Map(AggregatorTokenStatusChangeDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "AggregatorTokenStatusChangeDto is null");

        var notification = new TokenStatusChange
        {
            TokenChangeStatusId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo)
        };

        return notification;
    }

    private static TokenStatusChangeDetails MapDetails(AggregatorTokenStatusChangeDetailsDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("Details is null");
            return null;
        }

        Console.WriteLine($"Mapping TokenStatusChangeDetails");

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