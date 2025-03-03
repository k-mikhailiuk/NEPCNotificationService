using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Aggregator.DTOs.Enums;
using Common.Mappers.Abstractions;

namespace Common.Mappers.Notifications;

public class CardIdentifierMapper : IDtoEntityMapper<AggregatorCardIdentifierDto, CardIdentifier>
{
    private readonly IDtoEntityMapper<AggregatorCardIdentifierType, CardIdentifierType> _cardIdentifierTypeMapper;
    
    public CardIdentifierMapper(IDtoEntityMapper<AggregatorCardIdentifierType, CardIdentifierType> mapper)
    { 
        _cardIdentifierTypeMapper = mapper;
    }
    
    public CardIdentifier Map(AggregatorCardIdentifierDto dto)
    {
        return new CardIdentifier
        {
            CardIdentifierType = _cardIdentifierTypeMapper.Map(dto.Type),
            CardIdentifierValue = dto.Value
        };
    }
}