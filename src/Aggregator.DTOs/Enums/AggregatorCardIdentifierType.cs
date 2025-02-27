namespace Aggregator.DTOs.Enums;

public enum AggregatorCardIdentifierType
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
    /// Токен карты
    /// </summary>
    dpan = 2,
    
    /// <summary>
    /// Хэш номера карты в верхнем регистре, вычисленного алгоритмом sha-256
    /// </summary>
    sha256 = 3,
    
    /// <summary>
    /// Маскированный токен карты
    /// </summary>
    dpanMask = 4,
    
    /// <summary>
    /// Хэш номера карты в верхнем регистре, вычисленного алгоритмом sha-1
    /// </summary>
    sha1 = 5,
    
    /// <summary>
    /// Хэш токена в верхнем регистре, вычисленного алгоритмом sha-1
    /// </summary>
    dpanSha1 = 6,
    
    /// <summary>
    /// Номер счета+последние 4 цифры ПАНа через разделитель '='
    /// </summary>
    acctIdPanTail = 7,
    
    /// <summary>
    /// EAN13 карты (Штрих-код)
    /// </summary>
    ean13 = 8,
}