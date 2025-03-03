using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.DTOs.OwiUserAction;
using Aggregator.DTOs.PinChange;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.DTOs.Unhold;

namespace Common.Mappers;

public static class NotificationTypeMapper
{
    public static readonly Dictionary<NotificationType, Type> MappingDictionary = new()
    {
        { NotificationType.IssFinAuth, typeof(AggregatorIssFinAuthDto) },
        { NotificationType.AcqFinAuth, typeof(AggregatorAcqFinAuthDto) },
        { NotificationType.CardStatusChange, typeof(AggregatorCardStatusChangeDto) },
        { NotificationType.PinChange, typeof(AggregatorPinChangeDto) },
        { NotificationType.TokenStatusChange, typeof(AggregatorTokenStausChangeDto) },
        { NotificationType.Unhold, typeof(AggregatorUnholdDto) },
        { NotificationType.OwiUserAction, typeof(AggregatorOwiUserActionDto) },
        { NotificationType.AcctBalChange, typeof(AggregatorAcctBalChangeDto) }
    };
}