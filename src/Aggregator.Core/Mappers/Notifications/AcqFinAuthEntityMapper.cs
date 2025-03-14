using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.AcqFinAuth;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class AcqFinAuthEntityMapper : INotificationMapper<AcqFinAuth, AggregatorAcqFinAuthDto>
{
    private readonly ILogger<AcqFinAuthEntityMapper> _logger;

    public AcqFinAuthEntityMapper(ILogger<AcqFinAuthEntityMapper> logger)
    {
        _logger = logger;
    }

    public AcqFinAuth Map(AggregatorAcqFinAuthDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("AcqFinAuthDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorAcqFinAuthDto is null");
        }

        var notification = new AcqFinAuth
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.AcqFinAuth),
            MerchantInfo = MerchantInfoMapper.MapMerchantInfo(dto.MerchantInfo)
        };

        return notification;
    }

    private AcqFinAuthDetails MapDetails(AggregatorAcqFinAuthDetailsDto dto)
    {
        if (dto == null)
        {
            _logger.LogInformation("Details is null");
            return null;
        }

        _logger.LogInformation("Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}", dto.Id, dto.TransactionTime);

        return new AcqFinAuthDetails
        {
            AcqFinAuthDetailsId = dto.Id,
            Reversal = Convert.ToBoolean(dto.Reversal),
            TransType = dto.TransType,
            ExpDate = dto.ExpDate,
            AccountId = dto.AccountId,
            CorrespondingAccount = dto.CorrespondingAccount,
            AuthMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AuthMoney>(dto.AuthMoney),
            AuthDirection = dto.AuthDirection,
            LocalTime = ConversionExtensionsHelper.SafeConvertFromLocalToUtc(dto.LocalTime),
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime),
            ResponseCode = dto.ResponseCode,
            ApprovalCode = dto.ApprovalCode,
            Rrn = dto.RRN,
            AcqFee =  ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AcqFee>(dto.AcqFee),
            AcqFeeDirection = dto.AcqFeeDirection,
            ConvMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<ConvMoney>(dto.ConvMoney),
            PhysTerm = Convert.ToBoolean(dto.PhysTerm),
            AuthorizationCondition = dto.AuthorizationCondition,
            PosEntryMode = dto.PosEntryMode,
            ServiceId = dto.ServiceId,
            ServiceCode = dto.ServiceCode,
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}