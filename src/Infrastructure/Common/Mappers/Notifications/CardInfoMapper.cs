// using Aggregator.DataAccess.Entities;
// using Aggregator.DataAccess.Entities.OwnedEntities;
// using Aggregator.DTOs;
// using Common.Mappers.Abstractions;
//
// namespace Common.Mappers.Notifications;
//
// public class CardInfoMapper : IDtoEntityMapper<AggregatorCardInfoDto, CardInfo>
// {
//     private readonly IDtoEntityMapper<AggregatorCardIdentifierDto, CardIdentifier> _identifierMapper;
//
//     public CardInfoMapper(IDtoEntityMapper<AggregatorCardIdentifierDto, CardIdentifier> identifierMapper)
//     {
//         _identifierMapper = identifierMapper;
//     }
//
//     public CardInfo Map(AggregatorCardInfoDto dto)
//     {
//         return new CardInfo
//         {
//             ExpDate = dto.ExpDate,
//             Limits = dto.Limits,
//             CardIdentifier =
//                 _identifierMapper.Map((dto.CardIdentifier ?? throw new InvalidOperationException()).First()),
//             ContractId = dto.ContractId,
//             MobilePhone = dto.MobilePhone ?? string.Empty,
//             RefPan = dto.RefPan,
//         };
//     }
// }