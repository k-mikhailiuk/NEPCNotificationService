using Aggregator.Core.Mappers.Abstractions;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.Unhold;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления Unhold (<see cref="AggregatorUnholdDto"/>) в сущность <see cref="Unhold"/>.
/// </summary>
public class UnholdEntityMapper(ILogger<UnholdEntityMapper> logger) : INotificationMapper<Unhold, AggregatorUnholdDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorUnholdDto"/> в сущность <see cref="Unhold"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления Unhold.</param>
    /// <returns>Сущность <see cref="Unhold"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="dto"/> равен null.</exception>
    public Unhold Map(AggregatorUnholdDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorUnholdDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorUnholdDto is null");
        }

        var notification = new Unhold
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = ConversionExtensionsHelper.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = ConversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.Unhold),
            CardInfo = CardInfoMapper.MapCardInfo(dto.CardInfo),
            MerchantInfo = MerchantInfoMapper.MapMerchantInfo(dto.MerchantInfo),
            NotificationType = NotificationType.Unhold,
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей уведомления Unhold (<see cref="AggregatorUnholdDetailsDto"/>) в сущность <see cref="UnholdDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления Unhold.</param>
    /// <returns>
    /// Сущность <see cref="UnholdDetails"/> с заполненными данными или null, если <paramref name="dto"/> равен null.
    /// </returns>
    private UnholdDetails MapDetails(AggregatorUnholdDetailsDto dto)
    {
        if (dto == null)
        {
            logger.LogInformation("AggregatorUnholdDetailsDto is null");
            return null;
        }

        logger.LogWarning($"Mapping Details: Id={dto.Id}, TransactionTime={dto.TransactionTime}");

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
            LocalTime = ConversionExtensionsHelper.SafeConvertFromLocalToUtc(dto.LocalTime),
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