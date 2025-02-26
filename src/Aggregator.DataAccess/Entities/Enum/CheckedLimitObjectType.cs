namespace Aggregator.DataAccess.Entities.Enum;

/// <summary>
/// Перечисление типов объектов, к которым применяется проверяемый лимит.
/// </summary>
public enum CheckedLimitObjectType
{
    /// <summary>
    /// Неопределённый тип объекта.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Лимит применяется к карте.
    /// </summary>
    Card = 1,

    /// <summary>
    /// Лимит применяется к счёту.
    /// </summary>
    Account = 2,

    /// <summary>
    /// Лимит применяется к группе карт.
    /// </summary>
    Card_Group = 3,

    /// <summary>
    /// Неизвестный тип объекта.
    /// </summary>
    Unknown = 4
}
