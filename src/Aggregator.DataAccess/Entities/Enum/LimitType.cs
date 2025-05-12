namespace Aggregator.DataAccess.Entities.Enum;

/// <summary>
/// Перечисление типов лимитов.
/// </summary>
public enum LimitType
{
    /// <summary>
    /// Неопределённый тип лимита.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Лимит по сумме (AmtLimit).
    /// </summary>
    AmtLimit = 1,

    /// <summary>
    /// Лимит по количеству (CntLimit).
    /// </summary>
    CntLimit = 2
}
