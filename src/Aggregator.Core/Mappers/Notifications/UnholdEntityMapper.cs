using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.Unhold;

namespace Aggregator.Core.Mappers.Notifications;

public class UnholdEntityMapper : INotificationMapper<Unhold, AggregatorUnholdDto>
{
    public Unhold Map(AggregatorUnholdDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "AggregatorTokenStatusChangeDto is null");

        var notification = new Unhold
        {
            UnholdId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            MerchantInfo = MerchantInfoMapper.MapMerchantInfo(dto.MerchantInfo)
        };

        return notification;
    }

    private static UnholdDetails MapDetails(AggregatorUnholdDetailsDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("Details is null");
            return null;
        }

        Console.WriteLine($"Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}");

        return new UnholdDetails
        {
            UnholdDetailsId = dto.Id,
            Reversal = Convert.ToBoolean(dto.Reversal),
            TransType = dto.TransType,
            CorrespondingAccount = dto.CorrespondingAccount,
            AccountId = dto.AccountId,
            AuthMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AuthMoney>(dto.AuthMoney),
            UnholdDirection = dto.UnholdDirection,
            UnholdMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<UnholdMoney>(dto.UnholdMoney),
            LocalTime = ConversionExtensionsHelper.SafeConvertTime(dto.LocalTime),
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime),
            ApprovalCode = dto.ApprovalCode,
            Rrn = dto.RRN,
            IssFee = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<IssFee>(dto.IssFee),
            IssFeeDirection = dto.IssFeeDirection,
            SvTrace = dto.SvTrace,
            WalletProvider = dto.WalletProvider != null
                ? new WalletProvider
                {
                    Id = dto.WalletProvider.PaymentSystemId,
                    PaymentSystem = dto.WalletProvider.PaymentSystem,
                }
                : new WalletProvider
                {
                    Id = null,
                    PaymentSystem = null
                },
            Dpan = dto.Dpan,
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}