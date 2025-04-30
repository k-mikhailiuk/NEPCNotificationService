using Aggregator.Core.Mappers.Abstractions;
using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs.AcsOtp;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers.Notifications;

/// <summary>
/// Маппер, преобразующий DTO уведомления AcsOtp (<see cref="AggregatorOtpDto"/>) в сущность <see cref="AcsOtp"/>.
/// </summary>
public class AcsOtpEntityMapper(
    ILogger<CardStatusChangeEntityMapper> logger,
    CardInfoMapper cardInfoMapper,
    ConversionExtensionsHelper conversionExtensionsHelper,
    DateTimeConverter dateTimeConverter)
    : INotificationMapper<AcsOtp, AggregatorOtpDto>
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorOtpDto"/> в сущность <see cref="AcsOtp"/>.
    /// </summary>
    /// <param name="dto">DTO уведомления AcsOtp.</param>
    /// <returns>Сущность <see cref="AcsOtp"/> с заполненными данными.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="dto"/> равен null.</exception>
    public AcsOtp Map(AggregatorOtpDto dto)
    {
        if (dto == null)
        {
            logger.LogWarning("AggregatorCardStatusChangeDto is null");
            throw new ArgumentNullException(nameof(dto), "AggregatorCardStatusChangeDto is null");
        }

        var notification = new AcsOtp
        {
            NotificationId = dto.Id,
            EventId = dto.EventId,
            Time = dateTimeConverter.SafeConvertTime(dto.Time),
            Details = MapDetails(dto.Details),
            Extensions = conversionExtensionsHelper.MapExtensions(dto.Extensions, dto.Id, NotificationType.AcsOtp),
            CardInfo = cardInfoMapper.MapCardInfo(dto.CardInfo),
            NotificationType = NotificationType.AcsOtp,
            MerchantInfo = MapMerchantInfo(dto.MerchantInfo)
        };

        return notification;
    }

    /// <summary>
    /// Преобразует DTO деталей AcsOtp (<see cref="AggregatorAcsOtpDetailsDto"/>) в сущность <see cref="AcsOtpDetails"/>.
    /// </summary>
    /// <param name="dto">DTO деталей уведомления AcsOtp.</param>
    /// <returns>Сущность <see cref="AcsOtpDetails"/> с заполненными данными.</returns>
    private AcsOtpDetails MapDetails(AggregatorAcsOtpDetailsDto dto)
    {
        var otpInfo = new OtpInfo
        {
            Otp = dto.OtpInfo.Otp,
            ExpirationTime = dateTimeConverter.SafeConvertTime(dto.OtpInfo.ExpirationTime),
        };

        var details = new AcsOtpDetails
        {
            AuthMoney = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AuthMoney>(dto.AuthMoney),
            TransactionTime = dateTimeConverter.SafeConvertTime(dto.TransactionTime),
            OtpInfo = otpInfo,
        };

        return details;
    }

    /// <summary>
    /// Преобразует DTO информации о мерчанте AcsOtp (<see cref="AggregatorAcsOtpMerchantInfoDto"/>) в сущность <see cref="AcsOtpMerchantInfo"/>.
    /// </summary>
    /// <param name="dto">DTO информации о мерчанте AcsOtp.</param>
    /// <returns>Сущность <see cref="AcsOtpMerchantInfo"/> с заполненными данными.</returns>
    private static AcsOtpMerchantInfo MapMerchantInfo(AggregatorAcsOtpMerchantInfoDto dto)
    {
        return new AcsOtpMerchantInfo
        {
            Name = dto.Name,
            Country = dto.Country,
            Url = dto.Url,
            MerchantId = dto.Id,
        };
    }
}