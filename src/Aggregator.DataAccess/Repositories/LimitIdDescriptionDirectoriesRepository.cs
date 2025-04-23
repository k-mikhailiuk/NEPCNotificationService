using Aggregator.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с LimitIdDescriptionDirectory.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ILimitIdDescriptionDirectoriesRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="LimitIdDescriptionDirectory"/>.
/// </remarks>
public class LimitIdDescriptionDirectoriesRepository(AggregatorDbContext context)
    : Repository<LimitIdDescriptionDirectory>(context), ILimitIdDescriptionDirectoriesRepository
{
    /// <summary>
    /// Асинхронно получает запись справочника описаний лимитов по коду лимита.
    /// </summary>
    /// <param name="limitCode">Код лимита.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Объект <see cref="LimitIdDescriptionDirectory"/>, если запись найдена; в противном случае, <c>null</c>.
    /// </returns>
    public async Task<LimitIdDescriptionDirectory?> GetByLimitCodeAsync(long limitCode, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(x=>x.LimitCode == limitCode, cancellationToken: cancellationToken);
    }
}