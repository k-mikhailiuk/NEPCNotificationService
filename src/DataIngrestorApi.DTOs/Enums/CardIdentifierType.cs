namespace DataIngrestorApi.DTOs.Enums;

public enum CardIdentifierType
{
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// Номер карты
    /// </summary>
    Pan = 1,
    
    /// <summary>
    /// Хэш номера карты в верхнем регистре, вычисленного алгоритмом sha-256
    /// </summary>
    Sha256 = 2,
    
    /// <summary>
    /// Хэш номера карты в верхнем регистре, вычисленного алгоритмом sha-1
    /// </summary>
    Sha1 = 3,
    
    /// <summary>
    /// Номер счета+последние 4 цифры ПАНа через разделитель '='
    /// </summary>
    AcctIdPanTail = 4,
    
    /// <summary>
    /// EAN13 карты (Штрих-код)
    /// </summary>
    Ean13 = 5,
}