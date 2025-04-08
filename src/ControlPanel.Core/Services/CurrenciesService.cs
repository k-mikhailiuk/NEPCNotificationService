using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

public class CurrenciesService(IUnitOfWork unitOfWork) : ICurrenciesService
{
    public async Task<List<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken)
    {
        var currencies = await unitOfWork.Currencies.GetAllAsync(cancellationToken);
        
        return currencies.ToList();
    }

    public async Task CreateCurrency(AddCurrencyDto dto, CancellationToken cancellationToken)
    {
        var currency = Currency.Create(dto.CurrencyCode, dto.CurrencyName, dto.CurrencySymbol);
        
        await unitOfWork.Currencies.AddAsync(currency, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task EditCurrency(EditCurrencyDto dto, CancellationToken cancellationToken)
    {
        var currency = await unitOfWork.Currencies.GetByCodeAsync(dto.CurrencyCode, cancellationToken);
        
        currency.CurrencyName = dto.CurrencyName;
        currency.CurrencySymbol = dto.CurrencySymbol;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteCurrency(int currencyCode, CancellationToken cancellationToken)
    {
        var currency = await unitOfWork.Currencies.GetByCodeAsync(currencyCode, cancellationToken);

        if (currency != null) unitOfWork.Currencies.Remove(currency);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}