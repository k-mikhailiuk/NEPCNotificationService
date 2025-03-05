using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DTOs.CardStatusChange;

namespace Aggregator.Core.Mappers.Notifications;

public class CardStatusChangeEntityMapper : INotificationMapper<CardStatusChange, AggregatorCardStatusChangeDto>
{
    public CardStatusChange Map(AggregatorCardStatusChangeDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");

        var notification = new CardStatusChange
        {
            CardStatusChangeId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
        };

        return notification;
    }

    private static CardStatusChangeDetails MapDetails(AggregatorCardStatusChangeDetailsDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("Details is null");
            return null;
        }

        Console.WriteLine($"Mapping CardStatusChangeDetails:");

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