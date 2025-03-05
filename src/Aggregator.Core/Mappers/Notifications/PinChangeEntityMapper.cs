using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DTOs.PinChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class PinChangeEntityMapper : INotificationMapper<PinChange, AggregatorPinChangeDto>
{
    private readonly ILogger<PinChangeEntityMapper> _logger;

    public PinChangeEntityMapper(ILogger<PinChangeEntityMapper> logger)
    {
        _logger = logger;
    }

    public PinChange Map(AggregatorPinChangeDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("AggregatorPinChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorPinChangeDto is null");

        }

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

    private PinChangeDetails MapDetails(AggregatorPinChangeDetailsDto dto)
    {
        if (dto == null)
        {
            _logger.LogInformation("PinChangeDetailsDto is null");
            return null;
        }

        _logger.LogInformation($"Mapping PinChangeDetails: TransactionTime={dto.TransactionTime}", dto.TransactionTime);

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