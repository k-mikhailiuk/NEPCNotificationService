using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;

namespace Aggregator.Core.Mappers.Notifications;

public class PinChangeEntityMapper : INotificationMapper<PinChange, AggregatorPinChangeDto>
{
    public PinChange Map(AggregatorPinChangeDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "AggregatorPinChangeDto is null");

        var notification = new PinChange
        {
            PinChangeId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo)
        };

        return notification;
    }

    private static PinChangeDetails MapDetails(AggregatorPinChangeDetailsDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("Details is null");
            return null;
        }

        Console.WriteLine($"Mapping PinChangeDetails: TransactionTime={dto.TransactionTime}");

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