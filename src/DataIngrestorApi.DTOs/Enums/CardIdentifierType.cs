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
    pan = 1,
    
    /// <summary>
    /// Хэш номера карты в верхнем регистре, вычисленного алгоритмом sha-256
    /// </summary>
    sha256 = 2,
    
    /// <summary>
    /// Хэш номера карты в верхнем регистре, вычисленного алгоритмом sha-1
    /// </summary>
    sha1 = 3,
    
    /// <summary>
    /// Номер счета+последние 4 цифры ПАНа через разделитель '='
    /// </summary>
    acctIdPanTail = 4,
    
    /// <summary>
    /// EAN13 карты (Штрих-код)
    /// </summary>
    ean13 = 5,
}