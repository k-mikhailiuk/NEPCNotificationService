using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs;
using Common.Mappers.Abstractions;

namespace Common.Mappers.Notifications;

// public class CardInfoLimitWrapperMapper : IDtoEntityMapper<AggregatorLimitWrapperDto, CardInfoLimitWrapper>
// {
//     public CardInfoLimitWrapper Map(AggregatorLimitWrapperDto dto)
//     {
//         
//         var cardInfoLimitWrapper = new CardInfoLimitWrapper
//         {
//             LimitType = dto.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
//             LimitId = dto.AmtLimit?.Id ?? dto.CntLimit?.Id,
//             Limit = MapLimit(dto)
//         };
//
//         return cardInfoLimitWrapper;
//     }
// }