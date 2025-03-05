using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DTOs.OwiUserAction;

namespace Aggregator.Core.Mappers.Notifications;

public class OwiUserActionEntityMapper : INotificationMapper<OwiUserAction, AggregatorOwiUserActionDto>
{
    public OwiUserAction Map(AggregatorOwiUserActionDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");

        var notification = new OwiUserAction
        {
            OwiUserActionId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo)
        };

        return notification;
    }

    private static OwiUserActionDetails MapDetails(AggregatorOwiUserActionDetailsDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("Details is null");
            return null;
        }

        Console.WriteLine($"Mapping CardStatusChangeDetails:");

        return new OwiUserActionDetails
        {
            Action = dto.Action,
            Login = dto.Login,
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime)
        };
    }
}