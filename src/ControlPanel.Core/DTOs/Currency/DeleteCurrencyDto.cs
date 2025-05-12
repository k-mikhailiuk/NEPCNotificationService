namespace ControlPanel.Core.DTOs.Currency;

/// <summary>
/// Модель удаления валюты
/// </summary>
public class DeleteCurrencyDto
{
    /// <summary>
    /// ISO-код валюты
    /// </summary>
    public int CurrencyCode { get; set; }
}