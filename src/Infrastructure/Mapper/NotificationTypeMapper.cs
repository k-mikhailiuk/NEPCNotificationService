using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.AcsOtp;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.DTOs.OwiUserAction;
using Aggregator.DTOs.PinChange;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.DTOs.Unhold;

namespace Mapper;

/// <summary>
/// Содержит отображения типов уведомлений на соответствующие DTO и Entity типы.
/// Используется для сопоставления типов уведомлений с их моделями в системе агрегатора.
/// </summary>
public static class NotificationTypeMapper
{
    /// <summary>
    /// Словарь для отображения значения перечисления <see cref="NotificationType"/> на соответствующий тип DTO.
    /// </summary>
    public static readonly Dictionary<NotificationType, Type> EnumAggregatorTypeMapping = new()
    {
        { NotificationType.IssFinAuth, typeof(AggregatorIssFinAuthDto) },
        { NotificationType.AcqFinAuth, typeof(AggregatorAcqFinAuthDto) },
        { NotificationType.CardStatusChange, typeof(AggregatorCardStatusChangeDto) },
        { NotificationType.PinChange, typeof(AggregatorPinChangeDto) },
        { NotificationType.TokenStatusChange, typeof(AggregatorTokenStatusChangeDto) },
        { NotificationType.Unhold, typeof(AggregatorUnholdDto) },
        { NotificationType.OwiUserAction, typeof(AggregatorOwiUserActionDto) },
        { NotificationType.AcctBalChange, typeof(AggregatorAcctBalChangeDto) },
        { NotificationType.AcsOtp, typeof(AggregatorOtpDto) }
    };
    
    /// <summary>
    /// Словарь для отображения типа DTO на соответствующий тип Entity (модель базы данных).
    /// </summary>
    public static readonly Dictionary<Type, Type> AggregatorTypeEntityTypeMapping = new()
    {
        { typeof(AggregatorIssFinAuthDto), typeof(IssFinAuth) },
        { typeof(AggregatorAcqFinAuthDto), typeof(AcqFinAuth) },
        { typeof(AggregatorCardStatusChangeDto), typeof(CardStatusChange) },
        { typeof(AggregatorPinChangeDto), typeof(PinChange) },
        { typeof(AggregatorTokenStatusChangeDto), typeof(TokenStatusChange) },
        { typeof(AggregatorUnholdDto), typeof(Unhold) },
        { typeof(AggregatorOwiUserActionDto), typeof(OwiUserAction) },
        { typeof(AggregatorAcctBalChangeDto), typeof(AcctBalChange) },
        { typeof(AggregatorOtpDto), typeof(AcsOtp) }
    };
}