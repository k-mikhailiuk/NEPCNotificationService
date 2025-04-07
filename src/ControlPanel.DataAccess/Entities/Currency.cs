using System.ComponentModel.DataAnnotations.Schema;

namespace ControlPanel.DataAccess.Entities;

public class Currency
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CurrencyCode { get; set; }
    
    public string CurrencyName { get; set; }
    
    public string CurrencySymbol { get; set; }
    
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