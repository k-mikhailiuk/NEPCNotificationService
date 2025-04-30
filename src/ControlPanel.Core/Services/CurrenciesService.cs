using ControlPanel.Core.DTOs.Currency;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

/// <inheritdoc/>
public class CurrenciesService(IControlPanelUnitOfWork controlPanelUnitOfWork) : ICurrenciesService
{
    /// <inheritdoc/>
    public async Task<List<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken)
    {
        var currencies = await controlPanelUnitOfWork.Currencies.GetAllAsync(cancellationToken);
        
        return currencies.ToList();
    }

    /// <inheritdoc/>
    public async Task CreateCurrency(AddCurrencyDto dto, CancellationToken cancellationToken)
    {
        var currency = Currency.Create(dto.CurrencyCode, dto.CurrencyName, dto.CurrencySymbol);
        
        await controlPanelUnitOfWork.Currencies.AddAsync(currency, cancellationToken);
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    /// <inheritdoc/>
    public async Task EditCurrency(EditCurrencyDto dto, CancellationToken cancellationToken)
    {
        var currency = await controlPanelUnitOfWork.Currencies.GetByCodeAsync(dto.CurrencyCode, cancellationToken);
        
        if(currency == null)
            return;
        
        currency.CurrencyName = dto.CurrencyName;
        currency.CurrencySymbol = dto.CurrencySymbol;
        
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    /// <inheritdoc/>
    public async Task DeleteCurrency(int currencyCode, CancellationToken cancellationToken)
    {
        var currency = await controlPanelUnitOfWork.Currencies.GetByCodeAsync(currencyCode, cancellationToken);

        if (currency != null) controlPanelUnitOfWork.Currencies.Remove(currency);
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}