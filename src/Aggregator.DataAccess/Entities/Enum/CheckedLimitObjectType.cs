namespace Aggregator.DataAccess.Entities.Enum;

/// <summary>
/// Перечисление типов объектов, к которым применяется проверяемый лимит.
/// </summary>
public enum CheckedLimitObjectType
{
    /// <summary>
    /// Карта
    /// </summary>
    CARD = 0,
    
    /// <summary>
    /// Счет
    /// </summary>
    ACCOUNT = 1,
    
    /// <summary>
    /// Группа карт
    /// </summary>
    CARD_GROUP = 2,

    /// <summary>
    /// Неизвестный тип объекта.
    /// </summary>
    Unknown = 3
}
