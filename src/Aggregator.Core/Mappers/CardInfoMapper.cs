using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs;

namespace Aggregator.Core.Mappers;

/// <summary>
/// Класс для маппинга данных CardInfo из DTO в сущность.
/// </summary>
public static class CardInfoMapper
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorCardInfoDto"/> в экземпляр <see cref="CardInfo"/>.
    /// </summary>
    /// <param name="dto">Объект DTO для маппинга данных CardInfo.</param>
    /// <returns>
    /// Возвращает экземпляр <see cref="CardInfo"/>, заполненный данными из <paramref name="dto"/>.
    /// Если <paramref name="dto"/> равен null, возвращается null.
    /// </returns>
    public static CardInfo MapCardInfo(AggregatorCardInfoDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("CardInfo is null");
            return null;
        }

        Console.WriteLine($"Mapping CardInfo, contractId={dto.ContractId}");

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
                    CycleLength = x.AmtLimit != null ? x.AmtLimit.CycleLength : 0,
                    CycleType = x.AmtLimit?.CycleType,
                    EndTime = x.AmtLimit != null
                        ? ConversionExtensionsHelper.SafeConvertTime(x.AmtLimit.EndTime)
                        : ConversionExtensionsHelper.SafeConvertTime(x.CntLimit.EndTime),
                    TrsValue = x.AmtLimit != null ? decimal.Round(x.AmtLimit.TrsAmount, 2) / 100 : x.CntLimit.TrsValue,
                    UsedValue = x.AmtLimit != null ? decimal.Round(x.AmtLimit.UsedAmount, 2) / 100 : x.CntLimit.UsedValue,
                }
            }).ToList(),
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}