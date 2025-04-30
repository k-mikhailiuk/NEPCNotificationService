using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers;

/// <summary>
/// Класс для маппинга данных CardInfo из DTO в сущность.
/// </summary>
public class CardInfoMapper(
    ILogger<CardInfoMapper> logger,
    ConversionExtensionsHelper conversionExtensionsHelper,
    DateTimeConverter dateTimeConverter)
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorCardInfoDto"/> в экземпляр <see cref="CardInfo"/>.
    /// </summary>
    /// <param name="dto">Объект DTO для маппинга данных CardInfo.</param>
    /// <returns>
    /// Возвращает экземпляр <see cref="CardInfo"/>, заполненный данными из <paramref name="dto"/>.
    /// Если <paramref name="dto"/> равен null, возвращается null.
    /// </returns>
    public CardInfo? MapCardInfo(AggregatorCardInfoDto? dto)
    {
        if (dto == null)
        {
            logger.LogInformation("CardInfo is null. dto: {dto}", dto);
            return null;
        }

        logger.LogInformation("Mapping CardInfo, contractId={dto.ContractId}", dto.ContractId);

        return new CardInfo
        {
            ExpDate = dto.ExpDate,
            RefPan = dto.RefPan,
            ContractId = dto.ContractId,
            MobilePhone = dto.MobilePhone,
            Limits = dto.Limits?.Select(x => new CardInfoLimitWrapper
            {
                LimitType = x.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                Limit = new Limit
                {
                    LimitId = x.AmtLimit?.Id ?? x.CntLimit.Id,
                    LimitType = x.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                    Currency = x.AmtLimit?.Currency ?? null,
                    CycleLength = x.AmtLimit?.CycleLength ?? x.CntLimit?.CycleLength ?? 0,
                    CycleType = x.AmtLimit?.CycleType ?? x.CntLimit?.CycleType ?? null,
                    EndTime = x.AmtLimit != null
                        ? dateTimeConverter.SafeConvertTime(x.AmtLimit.EndTime)
                        : dateTimeConverter.SafeConvertTime(x.CntLimit.EndTime),
                    TrsValue = x.AmtLimit != null ? decimal.Round(x.AmtLimit.TrsAmount, 2) / 100 : x.CntLimit.TrsValue,
                    UsedValue = x.AmtLimit != null
                        ? decimal.Round(x.AmtLimit.UsedAmount, 2) / 100
                        : x.CntLimit.UsedValue,
                }
            }).ToList(),
            CardIdentifier = conversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}