using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Aggregator.DTOs.IssFinAuth;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

public class IssFinAuthEntityMapper : INotificationMapper<IssFinAuth, AggregatorIssFinAuthDto>
{
    private readonly ILogger<IssFinAuthEntityMapper> _logger;

    public IssFinAuthEntityMapper(ILogger<IssFinAuthEntityMapper> logger)
    {
        _logger = logger;
    }

    public IssFinAuth Map(AggregatorIssFinAuthDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("AggregatorIssFinAuthDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorIssFinAuthDto is null");
        }

        var notification = new IssFinAuth
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            MerchantInfo = MerchantInfoMapper.MapMerchantInfo(dto.MerchantInfo),
            AccountsInfo = MapAccountsInfo(dto.AccountsInfo),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.IssFinAuth),
            NotificationType = NotificationType.IssFinAuth,
        };

        return notification;
    }

    private IssFinAuthDetails MapDetails(AggregatorIssFinAuthDetailsDto dto)
    {
        if (dto == null)
        {
            _logger.LogInformation("Details is null");
            return null;
        }

        _logger.LogInformation($"Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}", dto.Id, dto.TransactionTime);

        try
        {
            return new IssFinAuthDetails
            {
                IssFinAuthDetailsId = dto.Id,
                Reversal = Convert.ToBoolean(dto.Reversal),
                TransType = dto.TransType,
                IssInstId = dto.IssInstId,
                CorrespondingAccount = dto.CorrespondingAccount,
                AccountId = dto.AccountId,
                AuthMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AuthMoney>(dto.AuthMoney),
                AuthDirection = dto.AuthDirection,
                ConvMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<ConvMoney>(dto.ConvMoney),
                AccountBalance = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AccountBalance>(dto.AccountBalance),
                BillingMoney = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<BillingMoney>(dto.BillingMoney),
                LocalTime = ConversionExtensionsHelper.SafeConvertFromLocalToUtc(dto.LocalTime),
                TransactionTime = ConversionExtensionsHelper.SafeConvertTime(dto.TransactionTime),
                ResponseCode = dto.ResponseCode,
                ApprovalCode = dto.ApprovalCode,
                Rrn = dto.RRN,
                AcqFee = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AcqFee>(dto.AcqFee),
                AcqFeeDirection = dto.AcqFeeDirection,
                SvTrace = dto.SvTrace,
                AuthorizationCondition = dto.AuthorizationCondition,
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
                CheckedLimits = dto.CheckedLimits?.Select(x => new CheckedLimit
                    {
                        Id = x.Id,
                        ObjectType = Enum.TryParse<CheckedLimitObjectType>(x.Type.ToString(), out var parsedType)
                            ? parsedType
                            : CheckedLimitObjectType.Unknown,
                        Exceeded = x.Exceeded
                    })
                    .ToList(),
                CardIdentifier = dto.CardIdentifier?
                    .Select(x => new CardIdentifier
                    {
                        CardIdentifierType = Enum.TryParse<CardIdentifierType>(x.Type.ToString(), out var parsedType)
                            ? parsedType
                            : null,
                        CardIdentifierValue = x.Value
                    })
                    .FirstOrDefault()!,
                IssFeeDirection = dto.IssFeeDirection,
                IssFee = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<IssFee>(dto.IssFee),
                AuthMoneyDetails = new AuthMoneyDetails
                {
                    ExceedLimitMoney =
                        ConversionExtensionsHelper.ConvertMoneyDtoToEntity<ExceedLimitMoney>(dto.AuthMoneyDetails
                            ?.ExceedLimitMoney),
                    OwnFundsMoney =
                        ConversionExtensionsHelper.ConvertMoneyDtoToEntity<OwnFundsMoney>(dto.AuthMoneyDetails
                            ?.OwnFundsMoney)
                }
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private List<IssFinAuthAccountsInfo> MapAccountsInfo(List<AggregatorAccountInfoDto> dto)
    {
        if (dto == null || dto.Count == 0)
        {
            _logger.LogInformation("AccountsInfo is null");
            return null;
        }

        _logger.LogInformation($"Mapping AccountsInfo");

        return dto.Select(x => new IssFinAuthAccountsInfo
        {
            AccountsInfoId = x.Id,
            Type = x.Type,
            AviableBalance = ConversionExtensionsHelper.ConvertMoneyDtoToEntity<AviableBalance>(x.AvailableBalance),
            ExceedLimit = x.ExceedLimit != null
                ? ConversionExtensionsHelper.ConvertMoneyDtoToEntity<ExceedLimitMoney>(x.ExceedLimit)
                : new ExceedLimitMoney { Amount = null, Currency = null },
            Limits = x.Limits != null
                ? x.Limits
                    .Select(l => new AccountsInfoLimitWrapper
                    {
                        LimitType = l.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                        LimitId = l.AmtLimit?.Id ?? l.CntLimit?.Id ?? 0,
                        Limit = new Limit
                        {
                            LimitId = l.AmtLimit?.Id ?? l.CntLimit?.Id ?? 0,
                            LimitType = l.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                            Currency = l.AmtLimit?.Currency ?? null,
                            CycleLength = l.AmtLimit != null ? l.AmtLimit.CycleLength : 0,
                            CycleType = l.AmtLimit != null ? l.AmtLimit.CycleType : null,
                            EndTime = l.AmtLimit != null
                                ? ConversionExtensionsHelper.SafeConvertTime(l.AmtLimit.EndTime)
                                : ConversionExtensionsHelper.SafeConvertTime(l.CntLimit.EndTime),
                            TrsValue = l.AmtLimit != null ? l.AmtLimit.TrsAmount : l.CntLimit.TrsValue,
                            UsedValue = l.AmtLimit != null ? l.AmtLimit.UsedAmount : l.CntLimit.UsedValue
                        }
                    }).ToList()
                : null,
        }).ToList();
    }
}