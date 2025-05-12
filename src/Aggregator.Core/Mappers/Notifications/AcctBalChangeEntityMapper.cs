using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.AcctBalChange;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер для преобразования DTO уведомления об изменении баланса счета (<see cref="AggregatorAcctBalChangeDto"/>) в сущность <see cref="AcctBalChange"/>.
/// </summary>
public class AcctBalChangeEntityMapper(
    ILogger<AcctBalChangeEntityMapper> logger,
    CardInfoMapper cardInfoMapper,
    ConversionExtensionsHelper conversionExtensionsHelper,
    MerchantInfoMapper merchantInfoMapper,
    DateTimeConverter dateTimeConverter,
    AccountsInfoMapper accountsInfoMapper)
    : INotificationMapper<AcctBalChange, AggregatorAcctBalChangeDto>
{
    /// <summary>
    /// Преобразует DTO уведомления <see cref="AggregatorAcctBalChangeDto"/> в сущность <see cref="AcctBalChange"/>.
    /// </summary>
    /// <param name="dto">Объект DTO уведомления об изменении баланса счета.</param>
    /// <returns>Сущность <see cref="AcctBalChange"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если входной параметр <paramref name="dto"/> равен null.</exception>
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
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions =
                conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.AcctBalChange),
            CardInfo = cardInfoMapper.MapCardInfo(dto.CardInfo),
            AccountsInfo = accountsInfoMapper.MapAccountsInfo(dto.AccountsInfo, dto.Id),
            NotificationType = NotificationType.AcctBalChange,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления в сущность <see cref="AcctBalChangeDetails"/>.
    /// </summary>
    /// <param name="dto">Объект DTO деталей уведомления.</param>
    /// <returns>Сущность <see cref="AcctBalChangeDetails"/> с заполненными данными или null, если dto равен null.</returns>
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
            TransactionTime = dateTimeConverter.SafeConvertTime(dto.TransactionTime),
            Auth = dto.Auth != null
                ? new Authorization { Reversal = Convert.ToBoolean(dto.Auth.Reversal), Id = dto.Auth.Id }
                : new Authorization { Reversal = null, Id = null },
            FinTrans = dto.FinTrans != null
                ? new FinTransaction
                {
                    FinTransactionId = dto.FinTrans.Id,
                    FeTrans = dto.FinTrans.FeTrans,
                    TranMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<TranMoney>(dto.FinTrans.TranMoney),
                    Direction = dto.FinTrans.Direction,
                    MerchantInfo = merchantInfoMapper.MapMerchantInfo(dto.FinTrans.MerchantInfo),
                    CorrespondingAccountType = dto.FinTrans.CorrespondingAccount
                }
                : null,
            IssInstId = dto.IssInstId,
            AccountId = dto.AccountId,
            AccountAmount = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AccountAmount>(dto.AccountAmount),
            Direction = dto.Direction,
            AccountBalance = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AccountBalance>(dto.AccountBalance),
        };
    }
}