namespace ControlPanel.Core.DTOs.Currency;

/// <summary>
/// DTO для добавления новой валюты.
/// </summary>
/// <remarks>
/// Содержит информацию о коде, наименовании и символе валюты.
/// </remarks>
public class AddCurrencyDto
{
    /// <summary>
    /// Получает или задаёт код валюты.
    /// </summary>
    public int CurrencyCode { get; set; }
    
    /// <summary>
    /// Получает или задаёт наименование валюты.
    /// </summary>
    public string CurrencyName { get; set; }
    
    /// <summary>
    /// Получает или задаёт символ валюты.
    /// </summary>
    public string CurrencySymbol { get; set; }
}