using Aggregator.DataAccess.Entities;
using Aggregator.DTOs;
using Aggregator.DTOs.IssFinAuth;
using Common.Mappers.Abstractions;

namespace Common.Mappers.Notifications.IssFinAuth;

public class IssFinAuthMapper : IDtoEntityMapper<AggregatorIssFinAuthDto, Aggregator.DataAccess.Entities.IssFinAuth.IssFinAuth>
{
    private readonly IDtoEntityMapper<AggregatorCardInfoDto, CardInfo> _cardInfoMapper;

    public IssFinAuthMapper(IDtoEntityMapper<AggregatorCardInfoDto, CardInfo> cardInfoMapper)
    {
        _cardInfoMapper = cardInfoMapper;
    }

    public Aggregator.DataAccess.Entities.IssFinAuth.IssFinAuth Map(AggregatorIssFinAuthDto dto)
    {
        return new Aggregator.DataAccess.Entities.IssFinAuth.IssFinAuth
        {
            IssFinAuthId = dto.Id,
            Details = dto.Details,
            Extensions = dto.Extensions,
            MerchantInfo = dto.MerchantInfo,
            CardInfo = dto.CardInfo == null ? null : _cardInfoMapper.Map(dto.CardInfo),
            Time = dto.Time,
            AccountsInfo = dto.AccountsInfo,
            EventId = dto.EventId,
            DetailsId = dto.Details.Id,
            CardInfoId = dto.CardInfo ?? _cardInfoMapper.Map(dto.CardInfo),
            MerchantInfoId = 1
        };
    }
}