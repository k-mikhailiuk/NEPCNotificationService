using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Aggregator.DTOs.IssFinAuth;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления IssFinAuth (<see cref="AggregatorIssFinAuthDto"/>) в сущность <see cref="IssFinAuth"/>.
/// </summary>
public class IssFinAuthEntityMapper(ILogger<IssFinAuthEntityMapper> logger)
    : INotificationMapper<IssFinAuth, AggregatorIssFinAuthDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorIssFinAuthDto"/> в сущность <see cref="IssFinAuth"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления IssFinAuth.</param>
    /// <returns>Сущность <see cref="IssFinAuth"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если dto равен null.</exception>
    public IssFinAuth Map(AggregatorIssFinAuthDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorIssFinAuthDto is null");
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
            AccountsInfo = MapAccountsInfo(dto.AccountsInfo, dto.Id),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.IssFinAuth),
            NotificationType = NotificationType.IssFinAuth,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления (<see cref="AggregatorIssFinAuthDetailsDto"/>) в сущность <see cref="IssFinAuthDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления IssFinAuth.</param>
    /// <returns>Сущность <see cref="IssFinAuthDetails"/> или null, если dto равен null.</returns>
    private IssFinAuthDetails MapDetails(AggregatorIssFinAuthDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("Details is null");
            return null;
        }

        logger.LogInformation($"Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}", dto.Id, dto.TransactionTime);

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

    /// <summary>
    /// Преобразует список DTO информации о счетах (<see cref="AggregatorAccountInfoDto"/>) в список сущностей <see cref="AccountsInfo"/>.
    /// </summary>
    /// <param name="dto">Список DTO информации о счетах.</param>
    /// <param name="notificationId">Идентификатор уведомления, к которому привязана информация о счетах.</param>
    /// <returns>Список сущностей <see cref="AccountsInfo"/> или null, если dto равен null или пуст.</returns>
    private List<AccountsInfo> MapAccountsInfo(List<AggregatorAccountInfoDto> dto, long notificationId)
    {
        if (dto == null || dto.Count == 0)
        {
            logger.LogInformation("AccountsInfo is null");
            return null;
        }

        logger.LogInformation($"Mapping AccountsInfo");

        return dto.Select(x => new AccountsInfo()
        {
            AccountsInfoId = x.Id,
            NotificationId = notificationId,
            NotificationType = NotificationType.IssFinAuth,
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
                    TrsValue = l.AmtLimit != null ? decimal.Round(l.AmtLimit.TrsAmount, 2) / 100 : l.CntLimit.TrsValue,
                    UsedValue = l.AmtLimit != null ? decimal.Round(l.AmtLimit.UsedAmount, 2) / 100 : l.CntLimit.UsedValue
                }
            }).ToList(),
        }).ToList();
    }
}