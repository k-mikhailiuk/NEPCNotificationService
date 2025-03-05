using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs;

namespace Aggregator.Core.Mappers;

public static class CardInfoMapper
{
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
            Limits = dto.Limits != null
                ? dto.Limits
                    .Select(x => new CardInfoLimitWrapper
                    {
                        LimitType = x.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                        LimitId = x.AmtLimit?.Id ?? x.CntLimit?.Id ?? 0,
                        Limit = new Limit
                        {
                            LimitId = x.AmtLimit?.Id ?? x.CntLimit?.Id ?? 0,
                            LimitType = x.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                            Currency = x.AmtLimit?.Currency ?? null,
                            CycleLength = x.AmtLimit != null ? x.AmtLimit.CycleLength : 0,
                            CycleType = x.AmtLimit != null ? x.AmtLimit.CycleType : null,
                            EndTime = x.AmtLimit != null
                                ? ConversionExtensionsHelper.SafeConvertTime(x.AmtLimit.EndTime)
                                : ConversionExtensionsHelper.SafeConvertTime(x.CntLimit.EndTime),
                            TrsValue = x.AmtLimit != null ? x.AmtLimit.TrsAmount : x.CntLimit.TrsValue,
                            UsedValue = x.AmtLimit != null ? x.AmtLimit.UsedAmount : x.CntLimit.UsedValue
                        }
                    }).ToList()
                : null,
            CardIdentifier = ConversionExtensionsHelper.MapCardIdentifier(dto.CardIdentifier)
        };
    }
}