using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.AcqFinAuth;

namespace Aggregator.Core.Mappers.Notifications;

public class AcqFinAuthEntityMapper : INotificationMapper<AcqFinAuth, AggregatorAcqFinAuthDto>
{
    public AcqFinAuth Map(AggregatorAcqFinAuthDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "AggregatorAcqFinAuthDto is null");

        var notification = new AcqFinAuth
        {
            AcqFinAuthId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            MerchantInfo = MerchantInfoMapper.MapMerchantInfo(dto.MerchantInfo)
        };

        return notification;
    }

    private static AcqFinAuthDetails MapDetails(AggregatorAcqFinAuthDetailsDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("Details is null");
            return null;
        }

        Console.WriteLine($"Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}");

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
            LocalTime = ConversionExtensionsHelper.SafeConvertTime(dto.LocalTime),
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime),
            ResponseCode = dto.ResponseCode,
            ApprovalCode = dto.ApprovalCode,
            Rrn = dto.RRN,
            AcqFee = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AcqFee>(dto.AcqFee),
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