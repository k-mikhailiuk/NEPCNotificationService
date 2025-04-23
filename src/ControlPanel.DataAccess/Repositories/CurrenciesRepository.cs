using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с Currency.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICurrenciesRepository"/> и наследует базовый класс <see cref="Repository{Currency}"/>.
/// </remarks>
public class CurrenciesRepository(ControlPanelDbContext context) : Repository<Currency>(context), ICurrenciesRepository
{
    /// <inheritdoc/>
    public async Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken)
        => await DbSet.FirstOrDefaultAsync(x => x.CurrencyCode == code, cancellationToken: cancellationToken);
}