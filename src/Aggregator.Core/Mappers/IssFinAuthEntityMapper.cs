using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DTOs.IssFinAuth;

namespace Aggregator.Core.Mappers;

public static class IssFinAuthEntityMapper
{
    public static IssFinAuth Map(AggregatorIssFinAuthDto dto)
    {
        return new IssFinAuth
        {
            IssFinAuthId = dto.Id,
            EventId = dto.EventId,
        };
    }
}