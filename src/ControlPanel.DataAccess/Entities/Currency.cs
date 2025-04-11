using System.ComponentModel.DataAnnotations.Schema;

namespace ControlPanel.DataAccess.Entities;

/// <summary>
/// Представляет сущность валюты с кодом, названием и символом валюты.
/// </summary>
public class Currency
{
    
    /// <summary>
    /// Код валюты. Значение не генерируется базой данных автоматически.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CurrencyCode { get; set; }
    
    /// <summary>
    /// Название валюты.
    /// </summary>
    public string CurrencyName { get; set; }
    
    /// <summary>
    /// Символ валюты.
    /// </summary>
    public string CurrencySymbol { get; set; }
    
    /// <summary>
    /// Создает новый экземпляр <see cref="Currency"/> с указанными параметрами.
    /// </summary>
    /// <param name="currencyCode">Код валюты.</param>
    /// <param name="currencyName">Название валюты. Не должно быть пустым или состоять только из пробелов.</param>
    /// <param name="currencySymbol">Символ валюты. Не должно быть пустым или состоять только из пробелов.</param>
    /// <returns>Новый объект <see cref="Currency"/> с заданными значениями.</returns>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если <paramref name="currencyName"/> или <paramref name="currencySymbol"/> равны <c>null</c> или содержат только пробелы.
    /// </exception>
    public static Currency Create(int currencyCode, string currencyName, string currencySymbol)
    {
        if (string.IsNullOrWhiteSpace(currencyName))
            throw new ArgumentException("CurrencyName cannot be null or whitespace.", nameof(currencyName));
        
        if (string.IsNullOrWhiteSpace(currencySymbol))
            throw new ArgumentException("CurrencySymbol cannot be null or whitespace.", nameof(currencySymbol));
        
        return new Currency
        {
            CurrencyCode = currencyCode,
            CurrencyName = currencyName,
            CurrencySymbol = currencySymbol
        }; 
    }
}