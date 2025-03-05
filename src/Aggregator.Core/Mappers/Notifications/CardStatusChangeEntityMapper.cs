using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DTOs.CardStatusChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class CardStatusChangeEntityMapper : INotificationMapper<CardStatusChange, AggregatorCardStatusChangeDto>
{
    private readonly ILogger<CardStatusChangeEntityMapper> _logger;

    public CardStatusChangeEntityMapper(ILogger<CardStatusChangeEntityMapper> logger)
    {
        _logger = logger;
    }

    public CardStatusChange Map(AggregatorCardStatusChangeDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("AggregatorCardStatusChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");
        }

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

    private CardStatusChangeDetails MapDetails(AggregatorCardStatusChangeDetailsDto dto)
    {
        if (dto == null)
        {
            _logger.LogInformation("Details is null");
            return null;
        }

        _logger.LogInformation("Mapping CardStatusChangeDetails:");

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