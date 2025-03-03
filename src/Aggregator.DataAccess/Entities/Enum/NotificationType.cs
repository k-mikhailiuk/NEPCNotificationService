namespace Aggregator.DataAccess.Entities.Enum;

/// <summary>
/// Тип уведомления
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// default
    /// </summary>
    Undefined = 0,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.IssFinAuth.IssFinAuth" />
    IssFinAuth = 1,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.AcqFinAuth.AcqFinAuth" />
    AcqFinAuth = 2,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.CardStatusChange.CardStatusChange" />
    CardStatusChange = 3,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.PinChange.PinChange" />
    PinChange = 4,
    
    /// <inheritdoc cref="TokenChangeStatus.TokenStatusChange" />
    TokenStatusChange = 5,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.Unhold.Unhold" />
    Unhold = 6,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.OwiUserAction.OwiUserAction" />
    OwiUserAction = 7,
    
    /// <inheritdoc cref="Aggregator.DataAccess.Entities.AcctBalChange.AcctBalChange" />
    AcctBalChange = 8
}