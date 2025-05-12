using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities.ABSEntities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с PushNotificationSettings.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IPushNotificationSettingsRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="PushNotificationSettings"/>.
/// </remarks>
public class PushNotificationSettingsRepository(AggregatorDbContext context)
    : Repository<PushNotificationSettings>(context), IPushNotificationSettingsRepository
{
    /// <summary>
    /// Асинхронно получает настройки пользователя по CustomerId.
    /// </summary>
    /// <param name="customerIds">Идентификатор клиента.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Асинхронная задача, возвращающая словарь CustomerID и их LanguageID как значение <see cref="long"/>, или null, если язык не найден.
    /// </returns>
    public async Task<Dictionary<int, int>> GetUserSettingsIds(
        IReadOnlyCollection<int> customerIds, CancellationToken cancellationToken)
        => await Context.Set<PushNotificationSettings>().Where(s => customerIds.Contains(s.CustomerID))
            .ToDictionaryAsync(
                s => s.CustomerID,
                s => s.LanguageID,
                cancellationToken
            );
}