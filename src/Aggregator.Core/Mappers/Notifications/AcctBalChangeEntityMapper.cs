using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.AcctBalChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class AcctBalChangeEntityMapper : INotificationMapper<AcctBalChange, AggregatorAcctBalChangeDto>
{
    private readonly ILogger<AcctBalChangeEntityMapper> _logger;

    public AcctBalChangeEntityMapper(ILogger<AcctBalChangeEntityMapper> logger)
    {
        _logger = logger;
    }

    public AcctBalChange Map(AggregatorAcctBalChangeDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("AcctBalChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");
        }

        var notification = new AcctBalChange
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.AcctBalChange),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo)
        };

        return notification;
    }

    private AcctBalChangeDetails MapDetails(AggregatorAcctBalChangeDetailsDto dto)
    {
        if (dto == null)
        {
            _logger.LogInformation("Details is null");
            return null;
        }

        _logger.LogInformation("Mapping CardStatusChangeDetails:");

        return new AcctBalChangeDetails
        {
            AcctBalChangeDetailsId = dto.Id,
            Reversal = Convert.ToBoolean(dto.Reversal),
            TransType = dto.TransType,
            TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime),
            Auth = dto.Auth != null
                ? new Authorization { Reversal = Convert.ToBoolean(dto.Auth.Reversal), Id = dto.Auth.Id }
                : new Authorization { Reversal = null, Id = null },
            FinTrans = dto.FinTrans != null
                ? new FinTransaction
                {
                    FinTransactionId = dto.FinTrans.Id,
                    FeTrans = dto.FinTrans.FeTrans,
                    TranMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<TranMoney>(dto.FinTrans.TranMoney),
                    Direction = dto.FinTrans.Direction,
                    MerchantInfo = MerchantInfoMapper.MapMerchantInfo(dto.FinTrans.MerchantInfo),
                    CorrespondingAccountType = dto.FinTrans.CorrespondingAccount
                } : null,
            IssInstId = dto.IssInstId,
            AccountId = dto.AccountId,
            AccountAmount = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AccountAmount>(dto.AccountAmount),
            Direction = dto.Direction,
            AccountBalance = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AccountBalance>(dto.AccountBalance),
        };
    }
}