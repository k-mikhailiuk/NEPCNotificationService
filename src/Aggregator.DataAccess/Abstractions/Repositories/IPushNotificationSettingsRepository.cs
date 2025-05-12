using Aggregator.DataAccess.Entities.ABSEntities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы с настройками push-уведомлений.
/// </summary>
public interface IPushNotificationSettingsRepository : IRepository<PushNotificationSettings>
{
    /// <summary>
    /// Возвращает сопоставление идентификаторов клиентов и их настроек push-уведомлений.
    /// </summary>
    /// <param name="customerIds">Набор идентификаторов клиентов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Словарь, где ключ — идентификатор клиента, значение — идентификатор записи настроек push-уведомлений.
    /// </returns>
    Task<Dictionary<int, int>> GetUserSettingsIds(IReadOnlyCollection<int> customerIds,
        CancellationToken cancellationToken);
}