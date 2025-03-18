using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.DTOs.OwiUserAction;
using Aggregator.DTOs.PinChange;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.DTOs.Unhold;

namespace Common;

public static class NotificationTypeMapper
{
    public static readonly Dictionary<NotificationType, Type> EnumAggregatorTypeMapping = new()
    {
        { NotificationType.IssFinAuth, typeof(AggregatorIssFinAuthDto) },
        { NotificationType.AcqFinAuth, typeof(AggregatorAcqFinAuthDto) },
        { NotificationType.CardStatusChange, typeof(AggregatorCardStatusChangeDto) },
        { NotificationType.PinChange, typeof(AggregatorPinChangeDto) },
        { NotificationType.TokenStatusChange, typeof(AggregatorTokenStatusChangeDto) },
        { NotificationType.Unhold, typeof(AggregatorUnholdDto) },
        { NotificationType.OwiUserAction, typeof(AggregatorOwiUserActionDto) },
        { NotificationType.AcctBalChange, typeof(AggregatorAcctBalChangeDto) }
    };
    
    public static readonly Dictionary<Type, Type> AggregatorTypeEntityTypeMapping = new()
    {
        { typeof(AggregatorIssFinAuthDto), typeof(IssFinAuth) },
        { typeof(AggregatorAcqFinAuthDto), typeof(AcqFinAuth) },
        { typeof(AggregatorCardStatusChangeDto), typeof(CardStatusChange) },
        { typeof(AggregatorPinChangeDto), typeof(PinChange) },
        { typeof(AggregatorTokenStatusChangeDto), typeof(TokenStatusChange) },
        { typeof(AggregatorUnholdDto), typeof(Unhold) },
        { typeof(AggregatorOwiUserActionDto), typeof(OwiUserAction) },
        { typeof(AggregatorAcctBalChangeDto), typeof(AcctBalChange) }
    };
}