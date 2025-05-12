using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.IssFinAuth;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления IssFinAuth (<see cref="AggregatorIssFinAuthDto"/>) в сущность <see cref="IssFinAuth"/>.
/// </summary>
public class IssFinAuthEntityMapper(
    ILogger<IssFinAuthEntityMapper> logger,
    CardInfoMapper cardInfoMapper,
    ConversionExtensionsHelper conversionExtensionsHelper,
    MerchantInfoMapper merchantInfoMapper,
    DateTimeConverter dateTimeConverter,
    AccountsInfoMapper accountsInfoMapper)
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
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            CardInfo = cardInfoMapper.MapCardInfo(dto.CardInfo),
            MerchantInfo = merchantInfoMapper.MapMerchantInfo(dto.MerchantInfo),
            AccountsInfo = accountsInfoMapper.MapAccountsInfo(dto.AccountsInfo, dto.Id),
            Extensions = conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.IssFinAuth),
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

        logger.LogInformation("Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}", dto.Id,
            dto.TransactionTime);

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
                AuthMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AuthMoney>(dto.AuthMoney),
                AuthDirection = dto.AuthDirection,
                ConvMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<ConvMoney>(dto.ConvMoney),
                AccountBalance = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AccountBalance>(dto.AccountBalance),
                BillingMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<BillingMoney>(dto.BillingMoney),
                LocalTime = dateTimeConverter.SafeConvertFromLocalToUtc(dto.LocalTime),
                TransactionTime = dateTimeConverter.SafeConvertTime(dto.TransactionTime),
                ResponseCode = dto.ResponseCode,
                ApprovalCode = dto.ApprovalCode,
                Rrn = dto.RRN,
                AcqFee = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AcqFee>(dto.AcqFee),
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
                IssFee = conversionExtensionsHelper.ConvertMoneyDtoToEntity<IssFee>(dto.IssFee),
                AuthMoneyDetails = new AuthMoneyDetails
                {
                    ExceedLimitMoney =
                        conversionExtensionsHelper.ConvertMoneyDtoToEntity<ExceedLimitMoney>(dto.AuthMoneyDetails
                            ?.ExceedLimitMoney),
                    OwnFundsMoney =
                        conversionExtensionsHelper.ConvertMoneyDtoToEntity<OwnFundsMoney>(dto.AuthMoneyDetails
                            ?.OwnFundsMoney)
                }
            };
        }
        catch (Exception e)
        {
            logger.LogError("Exception: {e}", e);
            throw;
        }
    }
}