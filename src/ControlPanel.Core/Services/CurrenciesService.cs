using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entites;

namespace ControlPanel.Core.Services;

public class CurrenciesService : ICurrenciesService
{
    private readonly IUnitOfWork _unitOfWork;

    public CurrenciesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken)
    {
        var keywords = await _unitOfWork.Currencies.GetAllAsync(cancellationToken);
        
        return keywords.ToList();
    }

    public async Task CreateCurrency(AddCurrencyDto dto, CancellationToken cancellationToken)
    {
        var currency = Currency.Create(dto.CurrencyCode, dto.CurrencyName, dto.CurrencySymbol);
        
        await _unitOfWork.Currencies.AddAsync(currency, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task EditCurrency(EditCurrencyDto dto, CancellationToken cancellationToken)
    {
        var currency = await _unitOfWork.Currencies.GetByCodeAsync(dto.CurrencyCode, cancellationToken);
        
        currency.CurrencyName = dto.CurrencyName;
        currency.CurrencySymbol = dto.CurrencySymbol;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteCurrency(int currencyCode, CancellationToken cancellationToken)
    {
        var currency = await _unitOfWork.Currencies.GetByCodeAsync(currencyCode, cancellationToken);

        if (currency != null) _unitOfWork.Currencies.Remove(currency);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}