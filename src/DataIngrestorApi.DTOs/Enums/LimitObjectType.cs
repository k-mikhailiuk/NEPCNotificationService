namespace DataIngrestorApi.DTOs.Enums;

public enum LimitObjectType
{
    /// <summary>
    /// Неизвестный тип объекта
    /// </summary>
    UNKNOWN = 0,
    
    /// <summary>
    /// Карта
    /// </summary>
    CARD = 1,
    
    /// <summary>
    /// Счет
    /// </summary>
    ACCOUNT = 2,
    
    /// <summary>
    /// Группа карт
    /// </summary>
    CARD_GROUP = 3
}