using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Aggregator.DTOs.AcctBalChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class AcctBalChangeEntityMapper(ILogger<AcctBalChangeEntityMapper> logger)
    : INotificationMapper<AcctBalChange, AggregatorAcctBalChangeDto>
{
    public AcctBalChange Map(AggregatorAcctBalChangeDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AcctBalChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");
        }

        var notification = new AcctBalChange
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.AcctBalChange),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            AccountsInfo = MapAccountsInfo(dto.AccountsInfo, dto.Id),
            NotificationType = NotificationType.AcctBalChange,
        };

        return notification;
    }

    private AcctBalChangeDetails MapDetails(AggregatorAcctBalChangeDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("Details is null");
            return null;
        }

        logger.LogInformation("Mapping CardStatusChangeDetails:");

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
    
     private List<AccountsInfo> MapAccountsInfo(List<AggregatorAccountInfoDto> dto, long notificationId)
    {
        if (dto == null || dto.Count == 0)
        {
            logger.LogInformation("AccountsInfo is null");
            return null;
        }

        logger.LogInformation("Mapping AccountsInfo");

        return dto.Select(x => new AccountsInfo()
        {
            AccountsInfoId = x.Id,
            NotificationId = notificationId,
            NotificationType = NotificationType.AcctBalChange,
            Type = x.Type,
            AviableBalance = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AviableBalance>(x.AvailableBalance),
            ExceedLimit = x.ExceedLimit != null
                ? ConversionExtensionsHelper.ConvertMoneyDtoToEntity<ExceedLimitMoney>(x.ExceedLimit)
                : new ExceedLimitMoney { Amount = null, Currency = null },
            Limits = x.Limits?.Select(l => new AccountsInfoLimitWrapper
            {
                LimitType = l.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                Limit = new Limit
                {
                    LimitId = l.AmtLimit?.Id ?? l.CntLimit?.Id ?? 0,
                    LimitType = l.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                    Currency = l.AmtLimit?.Currency ?? null,
                    CycleLength = l.AmtLimit != null ? l.AmtLimit.CycleLength : 0,
                    CycleType = l.AmtLimit?.CycleType,
                    EndTime = l.AmtLimit != null
                        ? ConversionExtensionsHelper.SafeConvertTime(l.AmtLimit.EndTime)
                        : ConversionExtensionsHelper.SafeConvertTime(l.CntLimit.EndTime),
                    TrsValue = l.AmtLimit?.TrsAmount ?? l.CntLimit.TrsValue,
                    UsedValue = l.AmtLimit?.UsedAmount ?? l.CntLimit.UsedValue
                }
            }).ToList(),
        }).ToList();
    }
}