using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.AcqFinAuth;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления AcqFinAuth (<see cref="AggregatorAcqFinAuthDto"/>) в сущность <see cref="AcqFinAuth"/>.
/// </summary>
public class AcqFinAuthEntityMapper(
    ILogger<AcqFinAuthEntityMapper> logger,
    ConversionExtensionsHelper conversionExtensionsHelper,
    MerchantInfoMapper merchantInfoMapper,
    DateTimeConverter dateTimeConverter)
    : INotificationMapper<AcqFinAuth, AggregatorAcqFinAuthDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorAcqFinAuthDto"/> в сущность <see cref="AcqFinAuth"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления AcqFinAuth.</param>
    /// <returns>Сущность <see cref="AcqFinAuth"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="dto"/> равен null.</exception>
    public AcqFinAuth Map(AggregatorAcqFinAuthDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AcqFinAuthDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorAcqFinAuthDto is null");
        }

        var notification = new AcqFinAuth
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.AcqFinAuth),
            MerchantInfo = merchantInfoMapper.MapMerchantInfo(dto.MerchantInfo),
            NotificationType = NotificationType.AcqFinAuth,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей AcqFinAuth (<see cref="AggregatorAcqFinAuthDetailsDto"/>) в сущность <see cref="AcqFinAuthDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления.</param>
    /// <returns>Сущность <see cref="AcqFinAuthDetails"/> или null, если dto равен null.</returns>
    private AcqFinAuthDetails MapDetails(AggregatorAcqFinAuthDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("Details is null");
            return null;
        }

        logger.LogInformation("Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}", dto.Id,
            dto.TransactionTime);

        return new AcqFinAuthDetails
        {
            AcqFinAuthDetailsId = dto.Id,
            Reversal = Convert.ToBoolean(dto.Reversal),
            TransType = dto.TransType,
            ExpDate = dto.ExpDate,
            AccountId = dto.AccountId,
            CorrespondingAccount = dto.CorrespondingAccount,
            AuthMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AuthMoney>(dto.AuthMoney),
            AuthDirection = dto.AuthDirection,
            LocalTime = dateTimeConverter.SafeConvertFromLocalToUtc(dto.LocalTime),
            TransactionTime = dateTimeConverter.SafeConvertTime(dto.TransactionTime),
            ResponseCode = dto.ResponseCode,
            ApprovalCode = dto.ApprovalCode,
            Rrn = dto.RRN,
            AcqFee = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AcqFee>(dto.AcqFee),
            AcqFeeDirection = dto.AcqFeeDirection,
            ConvMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<ConvMoney>(dto.ConvMoney),
            PhysTerm = Convert.ToBoolean(dto.PhysTerm),
            AuthorizationCondition = dto.AuthorizationCondition,
            PosEntryMode = dto.PosEntryMode,
            ServiceId = dto.ServiceId,
            ServiceCode = dto.ServiceCode,
            CardIdentifier = conversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}