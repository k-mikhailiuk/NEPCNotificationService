using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs.Enums;
using Common.Mappers.Abstractions;

namespace Common.Mappers.Notifications.Enums;

public class CardIdentifierTypeMapper : IDtoEntityMapper<AggregatorCardIdentifierType, CardIdentifierType>
{
    public CardIdentifierType Map(AggregatorCardIdentifierType dto)
    {
        return dto switch
        {
            AggregatorCardIdentifierType.Undefined => CardIdentifierType.Undefined,
            AggregatorCardIdentifierType.pan => CardIdentifierType.pan,
            AggregatorCardIdentifierType.dpan => CardIdentifierType.dpan,
            AggregatorCardIdentifierType.sha256 => CardIdentifierType.sha256,
            AggregatorCardIdentifierType.dpanMask => CardIdentifierType.dpanMask,
            AggregatorCardIdentifierType.sha1 => CardIdentifierType.sha1,
            AggregatorCardIdentifierType.dpanSha1 => CardIdentifierType.dpanSha1,
            AggregatorCardIdentifierType.acctIdPanTail => CardIdentifierType.acctIdPanTail,
            AggregatorCardIdentifierType.ean13 => CardIdentifierType.ean13,
            _ => throw new ArgumentOutOfRangeException(nameof(CardIdentifierType), dto, null)
        };
    }

}