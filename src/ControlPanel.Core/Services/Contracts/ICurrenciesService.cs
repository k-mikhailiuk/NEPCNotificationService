using ControlPanel.Core.DTOs;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

public interface ICurrenciesService
{
    Task<List<Currency>> GetCurrenciesAsync(CancellationToken cancellationToken);
    Task CreateCurrency(AddCurrencyDto dto, CancellationToken cancellationToken);
    Task EditCurrency(EditCurrencyDto dto, CancellationToken cancellationToken);

    Task DeleteCurrency(int currencyCode, CancellationToken cancellationToken);
}